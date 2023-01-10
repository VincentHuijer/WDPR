using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Stoel{
    [Key]
    public int StoelID { get; set; }
    public bool IsGereserveerd { get; set; }
    public int Rang { get; set; }
    public double Prijs { get; set; }

    public string Zaalnummer { get; set; }

    public Zaal? Zaal { get; set; }
    public ICollection<Zaal>? Zalen { get; set; }

    public Bestelling Bestelling { get; set; }

    public ICollection<Bestelling>? Bestellingen { get; set; }


    public string stoelReserveren(){
        return "geen logica toegevoegd";
    }


}