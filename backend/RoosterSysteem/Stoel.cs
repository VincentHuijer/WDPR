using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Stoel
{
    [Key]
    public int StoelID { get; set; }
    public bool IsGereserveerd { get; set; }
    public int Rang { get; set; }
    public double Prijs { get; set; }

    public int X { get; set; }
    public int Y { get; set; }

    public int Zaalnummer { get; set; }

    public Zaal? Zaal { get; set; }

    public List<BesteldeStoel>? BesteldeStoelen { get; set; }


    public string stoelReserveren()
    {
        return "geen logica toegevoegd";
    }


}