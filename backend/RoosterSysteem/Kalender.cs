using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Kalender
{
    public Kalender()
    {
        KalenderId = 0;
    }
    [Key]
    public int KalenderId { get; set; }
    public List<Show> Shows { get; set; }
    public string filterRooster()
    {
        return "geen logica toegevoegd";
    }
    public List<Show> overzicht()
    {
        return Shows;
    }

    public List<Show> HerhaalOptie(string? interval, int? aantalKeer, Show show)
    {
        List<Show> RepeatedVoorstellingen = new List<Show>();
        DateTime current = show.Datum;
        if (aantalKeer == 0 || interval == null)
        {
            RepeatedVoorstellingen.Add(show);
        }
        if (aantalKeer != null && interval != null)
        {
            for (int i = 0; i < aantalKeer; i++)
            {
                current = AddInterval(current, interval);
                Show newShow = new Show(show.Zaalnummer, show.Datum, show.VoorstellingId, show.KalenderId);
                newShow.Datum = current;
                RepeatedVoorstellingen.Add(newShow);
            }
        }

        return RepeatedVoorstellingen;
    }
    public DateTime AddInterval(DateTime current, string? interval)
    {
        switch (interval.ToLower())
        {
            case null:
                return current;
            case "weekly":
                return current = current.AddDays(7);
            case "monthly":
                return current = current.AddMonths(1);
            case "yearly":
                return current = current.AddYears(1);
            default:
                throw new ArgumentException("Invalid interval");
        }
    }

}
