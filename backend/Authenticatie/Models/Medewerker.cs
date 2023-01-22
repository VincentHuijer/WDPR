using System.Text.Json.Serialization;

namespace backend.Authenticatie;
public class Medewerker{
    public Medewerker(string voornaam, string achternaam, string email, string wachtwoord){
        Voornaam = voornaam;
        Achternaam = achternaam;
        Email = email;
        Wachtwoord = wachtwoord;
        //Rol = Rol.MedewerkerRol;
    }
    public int Id {set; get;}
    public string Voornaam {set; get;}
    public string Achternaam {set; get;}
    public string Email {set; get;}
    public string Wachtwoord {set; get;}
    public DateTime GeboorteDatum {set; get;}
    public Rol Rol {set; get;}
    public string RolNaam {set; get;}
    [JsonIgnore]
    public AccessToken? AccessToken {set; get;}
    public string? AccessTokenId {set; get;}
    public string? TwoFactorAuthSecretKey {set; get;}
    public bool TwoFactorAuthSetupComplete {set; get;}
    public bool IsBlocked {set; get;}
    public int Inlogpoging {set; get;}
    [JsonIgnore]
    public AuthenticatieToken? AuthenticatieToken {set; get;}
    public string? AuthenticatieTokenId {set; get;}
}