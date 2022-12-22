public class Klant{
    public Klant(string voornaam, string achternaam, string email, string wachtwoord){
        Voornaam = voornaam;
        Achternaam = achternaam;
        Email = email;
        Wachtwoord = wachtwoord;
        Rollen = new List<Rol>(){Rol.KlantRol};
        VerificatieToken = new VerificatieToken(){Token = "insertsomethingrandom", VerloopDatum = DateTime.Now.AddDays(3)};
    }

    public string Voornaam {set; get;}
    public string? Achternaam {set; get;}
    public string Email {set; get;}
    public string Wachtwoord {set; get;}
    public string? Beschrijving {set; get;}
    public string? Afbeelding {set; get;}
    public DateTime GeboorteDatum {set; get;}
    public bool Donateur {set; get;}
    public bool Artiest {set; get;}
    public List<Rol> Rollen {set; get;}
    public VerificatieToken? VerificatieToken {set; get;}
    public int Inlogpoging {get; set;} // Hier misschien iets mooiers op bedenken. Eventueel een inlogpoging class met info over de login poging erbij? Kan nuttig zijn voor logging en monitoring.
    //Toevoegen dat als inlogpoging > 3 ==> account geblokkeerd = true;
}