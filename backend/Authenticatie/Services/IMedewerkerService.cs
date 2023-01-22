namespace backend.Authenticatie;
public interface IMedewerkerService{
    Task<string> Login(string email, string wachtwoord, GebruikerContext context);
    Task<string> NieuweMedewerker(string voornaam, string achternaam, string email, string wachtwoord, GebruikerContext context);
    Task<(string, string)> Setup2FA(Medewerker medewerker, GebruikerContext context);
    Task<string> InitiatePasswordReset(Medewerker medewerker, GebruikerContext context);
    Task<string> ResetPassword(Medewerker medewerker, string token, string wachtwoord, GebruikerContext context);
    Task<string> Use2FA(Medewerker medewerker, string key);
}