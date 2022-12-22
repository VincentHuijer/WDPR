public interface IGebruikerService{

    Task<string> Registreer(string voornaam, string achternaam, string email, string wachtwoord, GebruikerContext context);
    Task<string> Login(string email, string wachtwoord, GebruikerContext context, bool isMedewerker);
    Task<string> Verifieer(string email, string token, GebruikerContext context);
    Task<bool> CheckDomainIsDisposable(string email);
}