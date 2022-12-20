public class Klant{

    public string Voornaam {set; get;}
    public string Achternaam {set; get;}
    public string Email {set; get;}
    public string Wachtwoord {set; get;}
    public string Beschrijving {set; get;}
    public string Afbeelding {set; get;}
    public DateTime GeboorteDatum {set; get;}
    public bool Donateur {set; get;}
    public bool Artiest {set; get;}
    public List<Rol> Rollen {set; get;}

}