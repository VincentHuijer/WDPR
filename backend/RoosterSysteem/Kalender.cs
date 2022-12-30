using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Kalender
{
    // public Kalender(/*List<Voorstelling> Voorstellingen*/)
    // {
    //     //voorstellingen = Voorstellingen;
    // }
    [Key]
    public int KalenderId { get; set; }
    public List<Voorstelling> Voorstellingen { get; set; }
    public string filterRooster()
    {
        return "geen logica toegevoegd";
    }
    public List<Voorstelling> overzicht()
    {
        return Voorstellingen;
    }

    public List<Voorstelling> HerhaalOptie(string interval, int aantalKeer, Voorstelling voor)
    {
        List<Voorstelling> RepeatedVoorstellingen = new List<Voorstelling>();
        DateTime current = voor.DatumEnTijd;
        if(aantalKeer == 0)
        {
            RepeatedVoorstellingen.Add(voor);
        }
        for(int i = 0; i < aantalKeer; i++)
        {
            AddInterval(current, interval);
            voor.DatumEnTijd = current;
            RepeatedVoorstellingen.Add(voor);
        }
        return RepeatedVoorstellingen;
    }
    public DateTime AddInterval(DateTime current, string interval)
    {
        switch (interval.ToLower())
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
