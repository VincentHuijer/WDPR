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
    private readonly IPermissionService _permissionService = new PermissionService();
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
            Klant klant = await _permissionService.GetKlantByEmailAsync(emailWachtwoord.Email, _context);
            if(klant == null) return HandleResponse("Error");
            if(klant.AccessTokenId == null){
                klant.AccessToken = new AccessToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(7)};
                await _context.SaveChangesAsync();
            }
            KlantInfo klantInfo = new KlantInfo(){TwoFactorAuthSetupComplete = klant.TwoFactorAuthSetupComplete, IsVerified = klant.TokenId == null? true : false, IsBlocked = klant.IsBlocked, AccessToken = await _permissionService.GetAccessTokenByTokenIdAsync(klant.AccessTokenId, _context),
            Voornaam = klant.Voornaam, Achternaam = klant.Achternaam, Email = klant.Email, Beschrijving = klant.Beschrijving, Afbeelding = klant.Afbeelding, GeboorteDatum = klant.GeboorteDatum, IsDonateur = klant.Donateur, IsArtiest = klant.Artiest, RolNaam = klant.RolNaam};
            return klantInfo;
        }
        var response = HandleResponse(responseString);
        return response;
    }

    [HttpPost("setup2fa")]
    public async Task<ActionResult<List<string>>> Setup2FA([FromBody] AccessTokenObject AccessToken) //Kunnen we hier ook de access token van klant aan meegeven die client side is opgeslagen en op basis daarvan de klant pakken? (Sidd)
    {
        
        Klant k = await _permissionService.GetKlantByAccessToken(AccessToken.AccessToken, _context);
        if(k == null) HandleResponse("UserNotFoundError");
        var res = await _service.Setup2FA(k, _context);
        if(res.Item1 == "" && res.Item2 == "") return HandleResponse("AlreadySetup2FA");
        List<string> responses = new List<string>();
        responses.Add(res.Item1);
        responses.Add(res.Item2);
        return responses;
    }

    [HttpPost("use2fa")]
    public async Task<ActionResult> Use2FA([FromBody] AccessTokenKey accessTokenKey){
        Klant k = await _permissionService.GetKlantByAccessToken(accessTokenKey.AccessToken, _context);
        if(k == null) HandleResponse("UserNotFoundError");
        var responseString = await _service.Use2FA(k, accessTokenKey.Key);

        if(responseString == "Success" && k.TwoFactorAuthSetupComplete == false){ //Voor de eerste keer 2fa gebruiken.
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

    [HttpGet("klanten")]
    public async Task<List<Klant>> GetKlantenAsync(){
        List<Klant> klanten = await _context.Klanten.ToListAsync();
        return klanten;
    }

    [HttpPost("klant/by/at")] //Get klant by accesstoken
    public async Task<ActionResult<KlantInfo>> GetKlantInfoByAT([FromBody] AccessTokenObject accessTokenObject){
        Klant klant = await _permissionService.GetKlantByAccessToken(accessTokenObject.AccessToken, _context);
        KlantInfo klantInfo = new KlantInfo(){TwoFactorAuthSetupComplete = klant.TwoFactorAuthSetupComplete, IsVerified = klant.TokenId == null? true : false, IsBlocked = klant.IsBlocked, AccessToken = await _permissionService.GetAccessTokenByTokenIdAsync(klant.AccessTokenId, _context),
        Voornaam = klant.Voornaam, Achternaam = klant.Achternaam, Email = klant.Email, Beschrijving = klant.Beschrijving, Afbeelding = klant.Afbeelding, GeboorteDatum = klant.GeboorteDatum, IsDonateur = klant.Donateur, IsArtiest = klant.Artiest, RolNaam = klant.RolNaam};
        return klantInfo;
    }

    [HttpPost("logoutall")]
    public async Task<ActionResult> LogoutAll([FromBody] AccessTokenObject accessTokenObject){
        Klant klant = await _permissionService.GetKlantByAccessToken(accessTokenObject.AccessToken, _context);
        if(klant == null) return HandleResponse("Error");
        AccessToken accessToken = await _context.AccessTokens.FirstOrDefaultAsync(a => a.Token == accessTokenObject.AccessToken);
        if(accessToken == null) return HandleResponse("Error");
        _context.AccessTokens.Remove(accessToken);
        klant.AccessToken = null;
        klant.AccessTokenId = null;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("request/passwordreset/{email}")]
    public async Task<ActionResult> InitiatePasswordReset(string email){
        Klant klant = await _permissionService.GetKlantByEmailAsync(email, _context);
        if(klant == null) return BadRequest();
        return HandleResponse(await _service.InitiatePasswordReset(klant, _context));
    }

    [HttpPost("complete/passwordreset/{email}")]
    public async Task<ActionResult> CompletePasswordReset([FromBody] AuthenticatieTokenNieuwWachtwoord authenticatieTokenNieuwWachtwoord, string email){
        Klant klant = await _permissionService.GetKlantByEmailAsync(email, _context);
        if(klant == null) return BadRequest();
        return HandleResponse(await _service.ResetPassword(klant, authenticatieTokenNieuwWachtwoord.AuthenticatieToken, authenticatieTokenNieuwWachtwoord.NieuwWachtwoord, _context));
    }

    [HttpPost("rol/by/at")]
    public async Task<ActionResult<string>> GetRolByAT([FromBody] AccessTokenObject accessTokenObject){
        Klant klant = await _permissionService.GetKlantByAccessToken(accessTokenObject.AccessToken, _context);
        string rol = klant.RolNaam;
        return rol;
    }

    public ActionResult HandleResponse(string response){ 
        var responses = ResponseList.Responses;
        if(responses.ContainsKey(response)){
            return StatusCode(responses[response].StatusCode, responses[response].Message);
        }
        return StatusCode(500);
    }

}

