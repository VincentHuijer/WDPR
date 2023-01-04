using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Authenticatie;
public class VerificatieToken{
    public VerificatieToken(){
        Console.WriteLine("nieuwe token");
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Token {set; get;}
    public DateTime VerloopDatum {set; get;}
    //public int? KlantId {set; get;} //Id van welke klant deze token is
    public Klant Klant {set; get;} //Klant die bij deze token hoort.
}