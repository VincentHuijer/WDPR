public interface IGebruikerService{

    Task<bool> Registreer(string voornaam, string achternaam, string email, string wachtwoord, GebruikerContext context);
    Task<bool> Login(string email, string wachtwoord, GebruikerContext context, bool isMedewerker);
    Task<bool> Verifieer(string email, string token, GebruikerContext context);

}