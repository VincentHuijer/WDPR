public class Medewerker{
    public Medewerker(string voornaam, string achternaam, string email, string wachtwoord){
        Voornaam = voornaam;
        Achternaam = achternaam;
        Email = email;
        Wachtwoord = wachtwoord;
        Rollen = new List<Rol>(){Rol.MedewerkerRol};
    }
    public string Voornaam {set; get;}
    public string Achternaam {set; get;}
    public string Email {set; get;} // Komt in contactgegevens
    public string Wachtwoord {set; get;}
    public string? Functie {set; get;}
    public string? ContactGegevens {set; get;} //Eigen class van maken
    public DateTime GeboorteDatum {set; get;}
    public string? Afbeelding {set; get;}
    public List<Rol> Rollen {set; get;}
}