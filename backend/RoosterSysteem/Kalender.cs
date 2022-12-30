using System;
using System.Collections.Generic;

public class Kalender
{
    public Kalender(List<Voorstelling> Voorstellingen)
    {
        voorstellingen = Voorstellingen;
    }

    public List<Voorstelling> voorstellingen { get; set; }
    public string filterRooster()
    {
        return "geen logica toegevoegd";
    }
    public List<Voorstelling> overzicht()
    {
        return voorstellingen;
    }

    public void HerhaalOptie(string interval, int aantalKeer, Voorstelling voor)
    {
        DateTime current = voor.DatumEnTijd;
        if(aantalKeer == 0)
        {
            voorstellingen.Add(voor);
        }
        for(int i = 0; i < aantalKeer; i++)
        {
            AddFrequency(current, interval);
            voor.DatumEnTijd = current;
            voorstellingen.Add(voor);
        }
    }
    public DateTime AddFrequency(DateTime current, string frequency)
    {
        switch (frequency.ToLower())
        {
            case "once":
                return current;
            case "weekly":
                return current.AddDays(7);
            case "monthly":
                return current.AddMonths(1);
            case "yearly":
                return current.AddYears(1);
            default:
                throw new ArgumentException("Invalid interval");
        }
    }

}
