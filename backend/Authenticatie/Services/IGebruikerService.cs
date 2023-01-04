namespace backend.Authenticatie;
public interface IGebruikerService{

    Task<string> Registreer(string voornaam, string achternaam, string email, string wachtwoord, VerificatieToken verificatieToken, GebruikerContext context);
    Task<string> Login(string email, string wachtwoord, GebruikerContext context);
    Task<string> Verifieer(string email, string token, GebruikerContext context);
    Task<(string, string)> Setup2FA(Klant klant, GebruikerContext context);
    Task<string> Use2FA(Klant klant, string key);
    Task<bool> CheckDomainIsDisposable(string email);
}