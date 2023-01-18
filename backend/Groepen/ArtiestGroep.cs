using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using backend.Authenticatie;
public class ArtiestGroep{
    [Key]
    public int GroepsId {set; get;}
    public string Omschrijving {set; get;}
    public string Groepsnaam {set; get;}
    [JsonIgnore]
    public List<Klant> Leden {set; get;}
    [JsonIgnore]
    public List<Show> Shows {set; get;}
}