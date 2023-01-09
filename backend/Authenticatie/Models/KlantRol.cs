using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Authenticatie;
public class KlantRol{

    [Key]
    [Column(Order = 0)]  // Define a composite primary key
    public int KlantId { get; set; }

    [Key]
    [Column(Order = 1)]
    public string RolNaam { get; set; }
    public Klant Klant {set; get;}
    public Rol Rol {set; get;}
}