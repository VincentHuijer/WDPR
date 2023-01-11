using System.ComponentModel.DataAnnotations;
using backend.Authenticatie;
public class ArtiestGroep{
    [Key]
    public int GroepsId {set; get;}
    public string Omschrijving {set; get;}
    public string Groepsnaam {set; get;}
    public List<Klant> Leden {set; get;}
    public List<Voorstelling> Voorstellingen {set; get;}
}