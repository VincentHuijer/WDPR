using System.Security.Cryptography;
using Google.Authenticator;
using Microsoft.EntityFrameworkCore;

namespace backend.Authenticatie;

public class MedewerkerService : IMedewerkerService
{
    private IEmailService _emailService = new EmailService();
    public async Task<string> Login(string email, string wachtwoord, GebruikerContext context)
    {
        Medewerker medewerker = await context.Medewerkers.FirstOrDefaultAsync(k => k.Email == email);
        if(medewerker == null) return "InvalidCredentialsError";
        if(medewerker.IsBlocked) return "UserBlockedError";
        else if(medewerker.Wachtwoord == wachtwoord){
                medewerker.Inlogpoging = 0;
                return "Success";
        }
        else if(medewerker.Wachtwoord != wachtwoord){
                medewerker.Inlogpoging++;
                if(medewerker.Inlogpoging >= 3) medewerker.IsBlocked = true;
        }
        return "InvalidCredentialsError";
    }

    public async Task<(string, string)> Setup2FA(Medewerker medewerker, GebruikerContext context)
    {
        if(medewerker.TwoFactorAuthSetupComplete) return ("", "");
        string key = GenerateRandomString(10);

        medewerker.TwoFactorAuthSecretKey = key;
        await context.SaveChangesAsync();

        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        SetupCode setupInfo = tfa.GenerateSetupCode("System", medewerker.Email, medewerker.TwoFactorAuthSecretKey, false, 3);

        string qrUrl = setupInfo.QrCodeSetupImageUrl;
        string manualEntryCode = setupInfo.ManualEntryKey;
        var tuple = (QrCodeUrl: qrUrl, ManualEntryCode: manualEntryCode);
        return tuple;
    }

    public async Task<string> Use2FA(Medewerker medewerker, string key)
    {
        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        bool result = tfa.ValidateTwoFactorPIN(medewerker.TwoFactorAuthSecretKey, key);
        if(result) return "Success";
        return "Invalid2FactorKeyError";
    }

    public async Task<string> InitiatePasswordReset(Medewerker medewerker, GebruikerContext context){
        if(medewerker.AuthenticatieTokenId != null){
            AuthenticatieToken authenticatieToken = context.AuthenticatieTokens.First(a => a.Token == medewerker.AuthenticatieTokenId);
            context.AuthenticatieTokens.Remove(authenticatieToken);
            await context.SaveChangesAsync();
        }
        medewerker.AuthenticatieToken = new AuthenticatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(1)};
        await context.SaveChangesAsync();
        await _emailService.Send(medewerker.Email, medewerker.AuthenticatieTokenId!, "Password Reset");
        return "Success";
    }
    public async Task<string> ResetPassword(Medewerker medewerker, string token, string wachtwoord, GebruikerContext context){
        AuthenticatieToken authenticatieToken = context.AuthenticatieTokens.FirstOrDefault(a => a.Token == token);
        if(authenticatieToken == null || authenticatieToken.VerloopDatum < DateTime.Now) return "Error"; //Goede error message
        if(medewerker.AuthenticatieTokenId != authenticatieToken.Token) return "Token matcht niet error"; //Goede error message
        medewerker.AuthenticatieToken = null;
        medewerker.AuthenticatieTokenId = null;
        medewerker.Wachtwoord = wachtwoord;
        await context.SaveChangesAsync();
        return "Success";
    }
    public static string GenerateRandomString(int length){
        var random = new byte[length];
        RandomNumberGenerator.Fill(random);
        string base32String = Convert.ToBase64String(random); //convert to base32string
        return base32String;
    }
}