public class Medewerker{
    public string Voornaam {set; get;}
    public string Achternaam {set; get;}
    public string Email {set; get;} // Komt in contactgegevens
    public string Wachtwoord {set; get;}
    public string Functie {set; get;}
    public string ContactGegevens {set; get;} //Eigen class van maken
    public DateTime GeboorteDatum {set; get;}
    public string Afbeelding {set; get;}
    public List<Rol> Rollen {set; get;}
}