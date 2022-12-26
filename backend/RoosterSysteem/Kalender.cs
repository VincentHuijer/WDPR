using System;
using System.Collections.Generic;

public class Kalender
{
    public Kalender(List<Voorstelling> Voorstellingen)
    {
        voorstellingen = Voorstellingen;
    }

    public List<Voorstelling> voorstellingen;
    public string filterRooster()
    {
        return "geen logica toegevoegd";
    }
    public List<Voorstelling> overzicht()
    {
        return voorstellingen;
    }

    public void HerhaalOptie(string interval, int repeat, Voorstelling voor)
    {
        DateTime current = voor.DatumEnTijd;
        for(int i = 0; i < repeat; i++)
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
