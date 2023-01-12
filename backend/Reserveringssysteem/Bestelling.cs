using System.ComponentModel.DataAnnotations;

public class Bestelling{

[Key]
public int BestellingId{get; set;}
public int Totaalbedrag{get; set;}
public DateTime BestelDatum { get; set; }
public Boolean isBetaald{get; set;}
public double kortingscode{get; set;}
public List<BesteldeStoel>? BesteldeStoelen {set; get;}

}