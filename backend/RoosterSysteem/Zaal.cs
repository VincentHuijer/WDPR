using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Zaal{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Zaalnummer { get; set; }
    public int BeschikbareRangen { get; set; }
    public List<Voorstelling>? Voorstellingen { get; set; }
    public List<Stoel> Stoelen { get; set; }

    public ICollection<VoorstellingRegel>? VoorstellingRegels { get; set; }

}