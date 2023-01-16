using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using backend.Authenticatie;

public class Bestelling{

[Key]
public int BestellingId{get; set;}
public int Totaalbedrag{get; set;}
public DateTime BestelDatum { get; set; }
public Boolean isBetaald{get; set;}
public double kortingscode{get; set;}
[JsonIgnore]
public List<BesteldeStoel>? BesteldeStoelen {set; get;}

public Klant Klant {set; get;}
public int KlantId {set; get;}
public bool IsActive {set; get;}
}