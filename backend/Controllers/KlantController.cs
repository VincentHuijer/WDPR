using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KlantController : ControllerBase
{
    //<<BELANGRIJK>>
    //WILLEN WE GEBRUIK MAKEN VAN DOTNET IDENTITY? IS DIT WEL NODIG? 
    //WE KUNNEN EIGEN HASHING MAKEN
    private readonly GebruikerContext _context;
    private IGebruikerService _service = new GebruikerService(new EmailService()); //Is er een andere manier om dit te doen?

    public KlantController(GebruikerContext context)
    {
        _context = context;
    }

    [HttpPost("registreer")]
    public async Task<ActionResult> NieuweKlant([FromBody] NieuweKlant klant)
    {   
        VerificatieToken verificatieToken = new VerificatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(3)};
        var response = HandleResponse(await _service.Registreer(klant.Voornaam, klant.Achternaam, klant.Email, klant.Wachtwoord, verificatieToken,  _context));
        return response;
    }

    [HttpPost("login")]
    public async Task<ActionResult<KlantInfo>> LoginKlant([FromBody] EmailWachtwoord emailWachtwoord)
    {
        var responseString = await _service.Login(emailWachtwoord.Email, emailWachtwoord.Wachtwoord/*Misschien dit wachtwoord gehashed opsturen?*/, _context);
        if(responseString == "Success"){
            Klant klant = await GetKlantByEmailAsync(emailWachtwoord.Email);
            if(klant == null) return HandleResponse("Error");
            if(klant.AccessTokenId == null){
                klant.AccessToken = new AccessToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(7)};
                await _context.SaveChangesAsync();
            }
            KlantInfo klantInfo = new KlantInfo(){TwoFactorAuthSetupComplete = klant.TwoFactorAuthSetupComplete, IsVerified = klant.TokenId == null? true : false, IsBlocked = klant.IsBlocked, AccessToken = await GetAccessTokenByTokenIdAsync(klant.AccessTokenId)};
            return klantInfo;
        }
        var response = HandleResponse(responseString);
        return response;
    }

    [HttpPost("setup2fa")]
    public async Task<ActionResult<List<string>>> Setup2FA([FromBody] string AccessToken) //Kunnen we hier ook de access token van klant aan meegeven die client side is opgeslagen en op basis daarvan de klant pakken? (Sidd)
    {
        
        Klant k = await GetKlantByAccessToken(AccessToken);
        if(k == null) HandleResponse("UserNotFoundError");
        var res = await _service.Setup2FA(k, _context);
        if(res.Item1 == "" && res.Item2 == "") return HandleResponse("AlreadySetup2FA");
        List<string> responses = new List<string>();
        responses.Add(res.Item1);
        responses.Add(res.Item2);
        return responses;
    }


    // [HttpPost("complete2fa")]
    // public async Task<ActionResult> Complete2FA([FromBody] string AccessToken)
    // {
    //     Klant klant = await GetKlantByAccessToken(AccessToken);
    //     if(klant == null) HandleResponse("UserNotFoundError");
    //     if(klant.TwoFactorAuthSetupComplete){
    //         return HandleResponse("AlreadySetup2FA");
    //     }else{
    //         klant.TwoFactorAuthSetupComplete = true;
    //         await _context.SaveChangesAsync();
    //         return HandleResponse("Success");
    //     }
    // }

    [HttpPost("use2fa")]
    public async Task<ActionResult> Use2FA([FromBody] AccessTokenKey accessTokenKey){ //Nog fixen dat hij deze 2 params accepteert.
        Klant k = await GetKlantByAccessToken(accessTokenKey.AccessToken);
        if(k == null) HandleResponse("UserNotFoundError");
        var responseString = await _service.Use2FA(k, accessTokenKey.Key);
        if(responseString == "Success"){
            k.TwoFactorAuthSetupComplete = true;
            await _context.SaveChangesAsync();
        }
        return HandleResponse(responseString);
    }

    [HttpPost("verifieer")]
    public async Task<ActionResult> VerifieerKlant([FromBody] EmailToken emailToken) 
    {
        var response = HandleResponse(await _service.Verifieer(emailToken.Email, emailToken.Token, _context));
        return response;
    }

    [HttpGet("controleer2fa")]
    public async Task<ActionResult> Controleer2FA([FromBody] string AccessToken){
        Klant k = await GetKlantByAccessToken(AccessToken);
        if(k.TwoFactorAuthSetupComplete){
            return HandleResponse("Success");
        }else if(!k.TwoFactorAuthSetupComplete) return HandleResponse("TwoFactorNotSetup");
        else{
            return BadRequest();
        }
    }

    [HttpGet("klanten")]
    public async Task<List<Klant>> GetKlantenAsync(){
        List<Klant> klanten = await _context.Klanten.ToListAsync();
        return klanten;
    }

    [HttpPost("reset")]
    public async Task<ActionResult> Reset(){
        var rows = _context.Klanten.ToList();
        foreach(var row in rows){
            _context.Klanten.Remove(row);
        }
        await _context.SaveChangesAsync();
        return Ok();
    }

    public ActionResult HandleResponse(string response){ 
        var responses = ResponseList.Responses;
        if(responses.ContainsKey(response)){
            return StatusCode(responses[response].Item1, responses[response].Item2);
        }
        return StatusCode(500);
    }
    //Wordt geen endpoint, is alleen nodig voor 2fa/login, misschien halen we het weg want evt niet nodig.
    public async Task<Klant> GetKlantByEmailAsync(string email){
        Klant k = await _context.Klanten.FirstOrDefaultAsync(k => k.Email == email);
        return k;
    }
    public async Task<Klant> GetKlantByAccessToken(string AccessToken){
        AccessToken accessToken = await _context.AccessTokens.FirstOrDefaultAsync(a => a.Token == AccessToken);
        if(accessToken == null) return null;
        Klant k = await _context.Klanten.FirstOrDefaultAsync(k => k.AccessToken == accessToken);
        if(k == null) return null; // error message weghalen, is voor debugging.
        else if(accessToken.VerloopDatum < DateTime.Now) return null;
        return k;
    }
    public async Task<AccessToken> GetAccessTokenByTokenIdAsync(string AccessTokenId){
        AccessToken accessToken = await _context.AccessTokens.FirstOrDefaultAsync(a => a.Token == AccessTokenId);
        return accessToken;
    }

}
public class KlantInfo{
    public bool TwoFactorAuthSetupComplete {set; get;}
    public bool IsVerified {set; get;}
    public bool IsBlocked {set; get;}
    public AccessToken AccessToken {set; get;}
}
public class EmailWachtwoord{
    public string Email {set; get;}
    public string Wachtwoord {set; get;}
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

public class ResponseList{
        //Custom namen toevoegen aan tuples
    public static Dictionary<string, Tuple<int, string>> Responses = new Dictionary<string, Tuple<int, string>>(){
        {"Success", Tuple.Create(200, "Success!")},
        {"AlreadyVerifiedError", Tuple.Create(403, "User already verified!")},
        {"UserNotFoundError", Tuple.Create(400, "User not found!")},
        {"ExpiredTokenError", Tuple.Create(403, "Token expired!")},
        {"NotVerifiedError", Tuple.Create(403, "User not verified!")},
        {"InvalidCredentialsError", Tuple.Create(401, "Email or password incorrect!")},
        {"DisposableMailError", Tuple.Create(406, "Disposable email used!")},
        {"EmailInUseError", Tuple.Create(409, "Email in use!")},
        {"AlreadySetup2FA", Tuple.Create(403, "User has already setup their 2FA!")},
        {"Invalid2FactorKeyError", Tuple.Create(401, "Invalid key used!")},
        {"UserBlockedError", Tuple.Create(401, "User has been blocked because of too many login attempts!")},
        {"TwoFactorNotSetup", Tuple.Create(200, "User hasn't set up 2FA")}
    };
}
