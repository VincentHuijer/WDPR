public interface IGebruikerService{

    void Registreer(string voornaam, string achternaam, string email, string wachtwoord);
    bool Login(string email, string wachtwoord);
    bool Verifieer(string email, string token);
    
}