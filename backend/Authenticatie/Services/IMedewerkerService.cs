namespace backend.Authenticatie;
public interface IMedewerkerService{
    Task<string> Login(string email, string wachtwoord, GebruikerContext context);
    Task<(string, string)> Setup2FA(Medewerker medewerker, GebruikerContext context);
    Task<string> Use2FA(Medewerker medewerker, string key);
}