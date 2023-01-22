using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Authenticatie;
public class Rol
{
    public static Rol KlantRol = new Rol() { Naam = "Klant" };
    public static Rol ArtiestRol = new Rol() { Naam = "Artiest" };
    public static Rol DonateurRol = new Rol() { Naam = "Donateur" };
    public static Rol MedewerkerRol = new Rol() { Naam = "Medewerker" };
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Naam { set; get; }
    public List<Klant> Klanten { set; get; }
    public List<Medewerker> Medewerkers { set; get; }
}
