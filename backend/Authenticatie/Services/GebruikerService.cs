using Microsoft.EntityFrameworkCore;
using Google.Authenticator;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace backend.Authenticatie;
public class GebruikerService : IGebruikerService{


    private IEmailService _emailService;
    public GebruikerService(IEmailService emailService){
        _emailService = emailService;
    }
    public async Task<string> Registreer(string voornaam, string achternaam, string email, string wachtwoord, VerificatieToken verificatieToken, GebruikerContext context){
        Klant klant = new Klant(voornaam, achternaam, email, wachtwoord){VerificatieToken = verificatieToken, TokenId = verificatieToken.Token};
        if(await CheckDomainIsDisposable(email)) return "DisposableMailError"; 
        else if(context.Klanten.Any(k => k.Email == email)) return "EmailInUseError"; 
        if(await context.Rollen.FirstOrDefaultAsync(r => r.Naam == "Klant") != null){
            klant.Rol = await context.Rollen.FirstAsync(r => r.Naam == "Klant");
        }else{
            klant.Rol = Rol.KlantRol;
        }
        await context.Klanten.AddAsync(klant);
        await context.SaveChangesAsync();
        await _emailService.Send(email, "https://theater-laak.netlify.app/verify?token=" + klant.VerificatieToken.Token + "&email="+ email, "Email Verification"); //Hier nog juiste content toevoegen
        return "Success";
    }
    public async Task<string> Login(string email, string wachtwoord, GebruikerContext context){
        Klant? klant = await context.Klanten.FirstOrDefaultAsync(k => k.Email == email);
        if(klant == null) return "InvalidCredentialsError";
        if(klant.IsBlocked) return "UserBlockedError";
        else if(klant.TokenId != null){
            return "NotVerifiedError";
        } 
        else if(klant.Wachtwoord == wachtwoord && klant.VerificatieToken == null){
                klant.Inlogpoging = 0;
                await context.SaveChangesAsync();
                return "Success";
        }
        else if(klant.Wachtwoord != wachtwoord && klant.VerificatieToken == null){
                klant.Inlogpoging++;
                await context.SaveChangesAsync();
                if(klant.Inlogpoging >= 3){
                    klant.IsBlocked = true;
                    await context.SaveChangesAsync();
                } 
        }
        return "InvalidCredentialsError";
    }
    public async Task<string> Verifieer(string email, string token, GebruikerContext context){
        Klant? klant = await context.Klanten.FirstOrDefaultAsync(k => k.Email == email);
        
        if(klant == null) return "UserNotFoundError";
        else if(klant.TokenId == null) return "AlreadyVerifiedError";
        VerificatieToken? VerificatieToken = await context.VerificatieTokens.FirstOrDefaultAsync(a => a.Token == klant.TokenId);
        if(VerificatieToken == null) return "ServerError";
        if(VerificatieToken.Token == token && VerificatieToken.VerloopDatum > DateTime.Now){
            klant.VerificatieToken = null;
            klant.TokenId = null;
            context.VerificatieTokens.Remove(VerificatieToken);
            await context.SaveChangesAsync();
            return "Success";
        }
        return "ExpiredTokenError"; 
    }

    public async Task<(string, string)> Setup2FA(Klant klant, GebruikerContext context){
        if(klant.TwoFactorAuthSetupComplete) return ("", "");
        string key = GenerateRandomString(10);

        klant.TwoFactorAuthSecretKey = key;
        await context.SaveChangesAsync();

        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        SetupCode setupInfo = tfa.GenerateSetupCode("System", klant.Email, klant.TwoFactorAuthSecretKey, false, 3);

        string qrUrl = setupInfo.QrCodeSetupImageUrl;
        string manualEntryCode = setupInfo.ManualEntryKey;
        var tuple = (QrCodeUrl: qrUrl, ManualEntryCode: manualEntryCode);
        return tuple;
    }
    public async Task<string> InitiatePasswordReset(Klant klant, GebruikerContext context){
        if(klant == null) return "Error";
        if(klant.AuthenticatieTokenId != null){
            AuthenticatieToken authenticatieToken = context.AuthenticatieTokens.First(a => a.Token == klant.AuthenticatieTokenId);
            context.AuthenticatieTokens.Remove(authenticatieToken);
            await context.SaveChangesAsync();
        }
        klant.AuthenticatieToken = new AuthenticatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(1)};
        await context.SaveChangesAsync();
        await _emailService.Send(klant.Email, "https://theater-laak.netlify.app/user/resetwachtwoord?token=" + klant.AuthenticatieTokenId! + "&email=" + klant.Email, "Password Reset");
        return "Success";
    }
    public async Task<string> ResetPassword(Klant klant, string token, string wachtwoord, GebruikerContext context){
        AuthenticatieToken authenticatieToken = context.AuthenticatieTokens.FirstOrDefault(a => a.Token == token);
        if(authenticatieToken == null || authenticatieToken.VerloopDatum < DateTime.Now) return "ExpiredTokenError"; 
        if(klant.AuthenticatieTokenId != authenticatieToken.Token) return "InvalidTokenError"; 
        klant.AuthenticatieToken = null;
        klant.AuthenticatieTokenId = null;
        klant.Wachtwoord = wachtwoord;
        await context.SaveChangesAsync();
        return "Success";
    }

    public static string GenerateRandomString(int length){
        var random = new byte[length];
        RandomNumberGenerator.Fill(random);
        string base32String = Convert.ToBase64String(random); 
        return base32String;
    }
    public async Task<string> Use2FA(Klant klant, string key){
        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        bool result = tfa.ValidateTwoFactorPIN(klant.TwoFactorAuthSecretKey, key);
        if(result) return "Success";
        return "Invalid2FactorKeyError";
    }   

    public async Task<bool> CheckDomainIsDisposable(string email){
        string apiUrl = "https://disposable.debounce.io/?email=" + email;
        HttpClient client = new HttpClient();
        var response = await client.GetAsync(apiUrl);
        dynamic result = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
        bool isDisposable = result.disposable;
        return isDisposable;
    }
}