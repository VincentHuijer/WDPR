using Microsoft.EntityFrameworkCore;
using Google.Authenticator;
using System.Security.Cryptography;
public class GebruikerService : IGebruikerService{


    private IEmailService _emailService;
    public GebruikerService(IEmailService emailService){
        _emailService = emailService;
    }
    public async Task<string> Registreer(string voornaam, string achternaam, string email, string wachtwoord, VerificatieToken verificatieToken, GebruikerContext context){
        Klant klant = new Klant(voornaam, achternaam, email, wachtwoord){VerificatieToken = verificatieToken};
        if(await CheckDomainIsDisposable(email)) return "DisposableMailError"; 
        else if(context.Klanten.Any(k => k.Email == email)) return "EmailInUseError"; 
        if(await context.Rollen.FirstOrDefaultAsync(r => r.Naam == "Klant") != null){
            klant.Rol = await context.Rollen.FirstAsync(r => r.Naam == "Klant");
        }else{
            klant.Rol = Rol.KlantRol;
        }
        await context.Klanten.AddAsync(klant);
        await context.SaveChangesAsync();
        //await _emailService.Send(email, klant.VerificatieToken.Token); //Hier nog juiste content toevoegen
        return "Success";
    }
    public async Task<string> Login(string email, string wachtwoord, GebruikerContext context){
        Klant? klant = await context.Klanten.FirstOrDefaultAsync(k => k.Email == email);
        if(klant == null) return "UserNotFoundError";
        if(klant.IsBlocked) return "UserBlockedError";
        else if(klant.TokenId != null){
            return "NotVerifiedError";
        } 
        else if(klant.Wachtwoord == wachtwoord && klant.VerificatieToken == null){
                klant.Inlogpoging = 0;
                if(!klant.TwoFactorAuthSetupComplete) return "2FA Setup Incomplete";
                return "Success";
        }
        else if(klant.Wachtwoord != wachtwoord && klant.VerificatieToken == null){
                klant.Inlogpoging++;
                if(klant.Inlogpoging >= 3) klant.IsBlocked = true;
        }
        return "InvalidCredentialsError";
    }
    public async Task<string> Verifieer(string email, string token, GebruikerContext context){
        Klant? klant = await context.Klanten.FirstOrDefaultAsync(k => k.Email == email);
        if(klant == null) return "UserNotFoundError";
        else if(klant.TokenId == null) return "AlreadyVerifiedError";
        else if(klant.VerificatieToken.Token == token && klant.VerificatieToken.VerloopDatum > DateTime.Now){
            klant.VerificatieToken = null;
            klant.TokenId = null;
            VerificatieToken vtoken = await context.VerificatieTokens.FirstAsync(vt => vt.Token == token);
            context.VerificatieTokens.Remove(vtoken);
            await context.SaveChangesAsync();
            return "Success";
        }
        return "ExpiredTokenError";
    }

    public async Task<(string, string)> Setup2FA(Klant klant, GebruikerContext context){
        
        string key = GenerateRandomString(10);

        klant.TwoFactorAuthSecretKey = key;
        await context.SaveChangesAsync();

        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        SetupCode setupInfo = tfa.GenerateSetupCode("System", klant.Email, klant.TwoFactorAuthSecretKey, true, 3);

        string qrUrl = setupInfo.QrCodeSetupImageUrl;
        string manualEntryCode = setupInfo.ManualEntryKey;
        var tuple = (QrCodeUrl: qrUrl, ManualEntryCode: manualEntryCode);
        return tuple;
    }

    public static string GenerateRandomString(int length){
        var random = new byte[length];
        RandomNumberGenerator.Fill(random);
        string base32String = Convert.ToBase64String(random) //convert to base32string
                                        .Replace('+', '=')
                                        .Replace('/', '=')
                                        .TrimEnd('=');
        return base32String;
    }
    public async Task<string> Use2FA(Klant klant, string key){
        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        bool result = tfa.ValidateTwoFactorPIN(klant.TwoFactorAuthSecretKey, key);
        if(result) return "Success";
        return "Invalid2FactorKeyError";
    }   

    public async Task<bool> CheckDomainIsDisposable(string email){
        string domain = email.Split('@')[1];
        string path = "./Authenticatie/disposable-emails.txt";
        string[] lines = await File.ReadAllLinesAsync(path);
        if(lines.Any(l => l==domain)){ //Als een van de lines gelijk is aan het domein van het doorgegeven email adres is het een temporary domain. 
            return true;
        }else{
            return false;
        }
    }
}