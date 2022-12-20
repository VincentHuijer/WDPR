public class Rol{
    public static Rol KlantRol = new Rol(){Naam = "Klant"};
    public static Rol ArtiestRol = new Rol(){Naam = "Artiest"};
    public static Rol DonateurRol = new Rol(){Naam = "Donateur"};
    public static Rol MedewerkerRol = new Rol(){Naam = "Medewerker"};
    public string Naam {set; get;}
}