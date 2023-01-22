using System.Text.Json.Serialization;

namespace backend.Authenticatie;
public class Klant{
    public Klant(string voornaam, string achternaam, string email, string wachtwoord){
        Voornaam = voornaam;
        Achternaam = achternaam;
        Email = email;
        Wachtwoord = wachtwoord;
    }

    public int Id {set; get;}
    public string Voornaam {set; get;}
    public string? Achternaam {set; get;}
    public string Email {set; get;}
    public string Wachtwoord {set; get;}
    public bool Donateur {set; get;}
    public bool Artiest {set; get;}
    public Rol Rol {set; get;}
    public string RolNaam {set; get;}
    public VerificatieToken? VerificatieToken {set; get;}
    public string? TokenId {set; get;}
    public int Inlogpoging {get; set;} 
    [JsonIgnore]
    public AccessToken? AccessToken {set; get;}
    public string? AccessTokenId {set; get;}
    public string? TwoFactorAuthSecretKey {set; get;}
    public bool TwoFactorAuthSetupComplete {set; get;}
    public bool IsBlocked {set; get;}
    [JsonIgnore]
    public AuthenticatieToken? AuthenticatieToken {set; get;}
    public string? AuthenticatieTokenId {set; get;}
    [JsonIgnore]
    public ArtiestGroep? ArtiestGroep {set; get;}
    public int? ArtiestGroepId {set; get;}
    [JsonIgnore]
    public List<Bestelling> Bestellingen {set; get;}
    [JsonIgnore]
    public List<Donatie> Donaties {set; get;}
}