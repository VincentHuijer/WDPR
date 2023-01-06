using System.Security.Cryptography;
using Google.Authenticator;
using Microsoft.EntityFrameworkCore;

namespace backend.Authenticatie;

public class MedewerkerService : IMedewerkerService
{
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
    public static string GenerateRandomString(int length){
        var random = new byte[length];
        RandomNumberGenerator.Fill(random);
        string base32String = Convert.ToBase64String(random); //convert to base32string
        return base32String;
    }
}