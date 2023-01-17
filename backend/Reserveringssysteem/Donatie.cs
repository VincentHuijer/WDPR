using System.Text.Json.Serialization;
using backend.Authenticatie;
public class Donatie{
    public int id {set; get;}
    public double Hoeveelheid {set; get;}
    public string Email {set; get;}
    public string Naam {set; get;}
    public DateTime Datum {set; get;}
    [JsonIgnore]
    public Klant Klant {set; get;}
    public int KlantId {set; get;}
}