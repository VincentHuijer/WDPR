using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Voorstelling{
    public Voorstelling() { }
    public Voorstelling(string titel,int zaalnummer, string omschrijving, double prijs)
    {
        VoorstellingTitel = titel;
        Zaalnummer = zaalnummer;
        Omschrijving = omschrijving;
        Prijs = prijs;
        BetrokkenPersonen = new List<string>();
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string VoorstellingTitel { get; set; }
    public List<string> BetrokkenPersonen { get; set; }
    public List<Klant> Kaartjeshouders { get; set; }
    public string Omschrijving { get; set; }
    public Klant Acteur { get; set; }
    public double Prijs { get; set; }
    public Kalender? Kalender { get; set; }
    public int KalenderId { get; set; }
    public int Zaalnummer { get; set; }
    public Zaal Zaal { get; set; }
    public DateTime DatumEnTijd { get; set; }
}