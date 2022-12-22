using System;
using System.Collections.Generic;
public class Voorstelling{
    public Voorstelling(string titel, string betrokkenPersonen, List<Klant> kaartjeshouders, string omschrijving, Klant acteur, Double prijs)
    {
        Titel = titel;
        BetrokkenPersonen = betrokkenPersonen;
        Kaartjeshouders = kaartjeshouders;
        Omschrijving = omschrijving;
        Acteur = acteur;
        Prijs = prijs;
    }
    public string Titel { get; set; }
    public string BetrokkenPersonen { get; set; }
    public List<Klant> Kaartjeshouders { get; set; }
    public string Omschrijving { get; set; }
    public Klant Acteur { get; set; }
    public Double Prijs { get; set; }

    public Voorstelling HerhaalOptie(string zaalnummer, DateTime DatumEnTijd)
    {
        return new Voorstelling(); // voeg logica toe
    }
}