using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Authenticatie;
public class AccessToken{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Token {set; get;}
    public DateTime VerloopDatum {set; get;}
    public Klant Klant {set; get;}
    public Medewerker Medewerker {set; get;}

}