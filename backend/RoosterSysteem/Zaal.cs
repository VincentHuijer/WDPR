using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Zaal{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Zaalnummer { get; set; }
    public string rooster { get; set; }
    public int beschikbareRangen { get; set; }
    public List<Voorstelling>? voorstellingen { get; set; }
    public List<Stoel> stoelen { get; set; }
    public string voegZaalToe(){
        return "geen logica toegevoegd";
    }
    public string verwijderZaal(){
        return "geen logica toegevoegd";
    }
}