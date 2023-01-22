namespace backend.Controllers;
using backend.Authenticatie;
using Microsoft.EntityFrameworkCore;
public class StoelData
{
    public int X { get; set; }
    public int Y { get; set; }
    public double Prijs { get; set; }
    public int Rang { get; set; }
    public bool IsGereserveerd { get; set; }
    public int StoelID { get; set; }
    public KlantInfoShort KlantInfo {set; get;}
}

public class NieuweVoorstelling
{
    public string Titel { get; set; }
    public string Omschrijving { get; set; }
    public string Image { get; set; }
    public string AccessToken {set; get;}
}

public class VoorstellingData
{
    public Voorstelling voorstelling { get; set; }
    public List<Show> shows { get; set; }
}

public class HerhaalbareShow{
    public int Zaalnummer { get; set; }
    public DateTime StartDatum { get; set; }
    public int VoorstellingId { get; set; }
    public string? Interval { get; set; }
    public int? AantalKeer { get; set; }
    public string AccessToken {set; get;}
}

public class nieuweMedewerker
{
    public string Voornaam { set; get; }
    public string Achternaam { set; get; }
    public string Email { set; get; }
    public string Wachtwoord { set; get; }
    public string AccessToken {set; get;}
}
public class AccessId{
    public string AccessToken {set; get;}
    public int Id {set; get;}
}

public class MedewerkerInfo
{
    public int Id {set; get;}
    public bool TwoFactorAuthSetupComplete { set; get; }
    public bool IsBlocked { set; get; }
    public AccessToken AccessToken { set; get; }
    public string Voornaam { set; get; }
    public string Achternaam { set; get; }
    public string Email { set; get; }
    public DateTime GeboorteDatum { set; get; }
    public string RolNaam { set; get; }
}

public class KlantInfo{
    public bool TwoFactorAuthSetupComplete {set; get;}
    public bool IsVerified {set; get;}
    public bool IsBlocked {set; get;}
    public AccessToken AccessToken {set; get;}
    public string Voornaam {set; get;}
    public string Achternaam {set; get;}
    public string Email {set; get;}
    public bool IsDonateur {set; get;}
    public bool IsArtiest {set; get;}
    public string RolNaam {set; get;}
    
}
public class EmailWachtwoord{
    public string Email {set; get;}
    public string Wachtwoord {set; get;}
}
public class AccessTokenObject{
    public string AccessToken {set; get;}
}
public class AuthenticatieTokenNieuwWachtwoord{
    public string AuthenticatieToken {set; get;}
    public string NieuwWachtwoord {set; get;}
}

public class NieuweKlant{
    public string Email {set; get;}
    public string Wachtwoord {set; get;}
    public string Voornaam {set; get;}
    public string Achternaam {set; get;}
}

public class EmailToken{
    public string Email {set; get;}
    public string Token {set; get;}
}
public class AccessTokenKey{
    public string AccessToken {set; get;}
    public string Key {set; get;}
}

public class NieuweGroep{
    public string Groepsnaam {set; get;}
    public string Omschrijving {set; get;}
    public string AccessToken {set; get;}
}

public class KlantInfoShort{
    public string Voornaam {set; get;}
    public string Achternaam {set; get;}
    public string Email {set; get;}
}

public class DonatieObject{
    public string Naam {set; get;}
    public string Email {set; get;}
    public double Hoeveelheid {set; get;}
}

public class Betaling
{
    public bool succes { get; set; }
    public int reference { get; set; }
}

public class BestellingBody
{
    public int Totaalbedrag { get; set; }
    public string BestelDatum { get; set; }

    public double Kortingscode { get; set; }

    public List<string> Stoelen { get; set; }
}

public class BestelInfo{
    public int ShowId {set; get;}
    public List<int> StoelIds {set; get;}
    public string AccessToken {set; get;}
}


public class ShowStoelen{
    public int ShowId {set; get;}
    public string ShowNaam {set; get;}
    public string ShowImage {set; get;}
    public DateTime Datum {set; get;}
    public List<Stoel> Stoelen {set; get;}
}
public class Kaartjeshouder{
    public string Voornaam {set; get;}
    public string Achternaam {set; get;}
    public string Email {set; get;}
    public List<StoelData> Stoelen {set; get;}
}
public class GroepsIdKlantAccess{
    public int GroepsId {set; get;}
    public int KlantId {set; get;}
    public string AccessToken {set; get;}
}

public class GroepsIdMailAccess{
    public int GroepsId {set; get;}
    public string KlantMail {set; get;}
    public string AccessToken {set; get;}
}
public class BestellingCleaner{
    public static async Task Clean(GebruikerContext _context){
                //Clean up old inactive and unpaid bestellingen
        bool anyOldActive = await _context.Bestellingen.Where(b => b.BestelDatum < DateTime.Now.AddMinutes(-10)).AnyAsync(b => b.IsActive);
        if(anyOldActive){
            var OldActiveBestellingen = await _context.Bestellingen.Where(b => b.BestelDatum < DateTime.Now.AddMinutes(-10)).Where(b => b.IsActive).ToListAsync();
            foreach (var b in OldActiveBestellingen){
                b.IsActive = false;
            }
            await _context.SaveChangesAsync();
        }
        bool anyUnpaidInactive = await _context.Bestellingen.Where(b => b.IsActive == false).AnyAsync(b => b.isBetaald == false);
        if(anyUnpaidInactive){
            var UnpaidInativeBestellingen = await _context.Bestellingen.Where(b => b.IsActive == false).Where(b => b.isBetaald == false).ToListAsync();
            foreach (var b in UnpaidInativeBestellingen){
                var SeatsToBeRemoved = await _context.BesteldeStoelen.Where(s => s.BestellingId == b.BestellingId).ToListAsync();
                _context.RemoveRange(SeatsToBeRemoved);
                _context.Remove(b);
            }
            await _context.SaveChangesAsync();
        }
    }
}

public class DonateurCheck{
    public static async Task DonateurStatusCheck(Klant klant, GebruikerContext _context){
        List<Donatie> DonatiesDitJaar = await _context.Donaties.Where(d => d.Datum > DateTime.Now.AddYears(-1)).Where(d => d.KlantId == klant.Id).ToListAsync();
        double totaal = 0;
        foreach(var Donatie in DonatiesDitJaar){
            totaal += Donatie.Hoeveelheid;
        }
        if(totaal >= 1000){
            if(klant.Donateur) return;
            klant.Donateur = true;
            klant.RolNaam = Rol.DonateurRol.Naam;
            await _context.SaveChangesAsync();
        }else{
            if(!klant.Donateur) return;
            klant.Donateur = false;
            klant.RolNaam = Rol.KlantRol.Naam;
            await _context.SaveChangesAsync();
            return;
        }
        return;
    }
}


public class ResponseList{
    public static Dictionary<string, (int StatusCode, string Message)> Responses = new Dictionary<string, (int StatusCode, string Message)>(){
        {"Success", (200, "Success!")},
        {"AlreadyVerifiedError", (403, "User already verified!")},
        {"UserNotFoundError", (400, "User not found!")},
        {"ExpiredTokenError", (403, "Token expired!")},
        {"NotVerifiedError", (403, "User not verified!")},
        {"InvalidCredentialsError", (401, "Email or password incorrect!")},
        {"DisposableMailError", (406, "Disposable email used!")},
        {"EmailInUseError", (409, "Email in use!")},
        {"AlreadySetup2FA", (403, "User has already setup their 2FA!")},
        {"Invalid2FactorKeyError", (401, "Invalid key used!")},
        {"UserBlockedError", (401, "User has been blocked because of too many login attempts!")},
        {"TwoFactorNotSetup", (200, "User hasn't set up 2FA")}
    };
}
