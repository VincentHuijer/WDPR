using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Authenticatie;
public class AccessToken{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Token {set; get;}
    public DateTime VerloopDatum {set; get;}
    [JsonIgnore]
    public Klant Klant {set; get;}
    [JsonIgnore]
    public Medewerker Medewerker {set; get;}

}