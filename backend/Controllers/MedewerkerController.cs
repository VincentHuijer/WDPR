using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedewerkerController : ControllerBase
{
    private readonly GebruikerContext _context;

    private IMedewerkerService _service = new MedewerkerService();

    public MedewerkerController(GebruikerContext context)
    {
        _context = context;
    }

    // ENDPOINTS
    [HttpPost("login")]
    public async Task<ActionResult<MedewerkerInfo>> LoginMedewerker([FromBody] EmailWachtwoord emailWachtwoord)
    {
        var responseString = await _service.Login(emailWachtwoord.Email, emailWachtwoord.Wachtwoord/*Misschien dit wachtwoord gehashed opsturen?*/, _context);
        if (responseString == "Success")
        {
            Medewerker medewerker = await GetMedewerkerByEmailAsync(emailWachtwoord.Email);
            if (medewerker == null) return HandleResponse("Error");
            if (medewerker.AccessTokenId == null)
            {
                medewerker.AccessToken = new AccessToken() { Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(7) };
                await _context.SaveChangesAsync();
            }

            MedewerkerInfo medewerkerInfo = new MedewerkerInfo()
            {
                TwoFactorAuthSetupComplete = medewerker.TwoFactorAuthSetupComplete,
                IsBlocked = medewerker.IsBlocked,
                AccessToken = await GetAccessTokenByTokenIdAsync(medewerker.AccessTokenId),
            };
            return medewerkerInfo;
        }

        var response = HandleResponse(responseString);
        return response;
    }

    [HttpPost("setup2fa")]
    public async Task<ActionResult<List<string>>> Setup2FA([FromBody] AccessTokenObject AccessToken) //Kunnen we hier ook de access token van klant aan meegeven die client side is opgeslagen en op basis daarvan de klant pakken? (Sidd)
    {

        Medewerker m = await GetMedewerkerByAccessToken(AccessToken.AccessToken);
        if (m == null) HandleResponse("UserNotFoundError");
        var res = await _service.Setup2FA(m, _context);
        if (res.Item1 == "" && res.Item2 == "") return HandleResponse("AlreadySetup2FA");
        List<string> responses = new List<string>();
        responses.Add(res.Item1);
        responses.Add(res.Item2);
        return responses;
    }

    [HttpPost("medewerker/by/at")] //Get medewerkerinfo by accesstoken
    public async Task<ActionResult<MedewerkerInfo>> GetKlantInfoByAT([FromBody] AccessTokenObject accessTokenObject){
        Medewerker medewerker = await GetMedewerkerByAccessToken(accessTokenObject.AccessToken);
        MedewerkerInfo medewerkerInfo = new MedewerkerInfo(){TwoFactorAuthSetupComplete = medewerker.TwoFactorAuthSetupComplete, IsBlocked = medewerker.IsBlocked, AccessToken = await GetAccessTokenByTokenIdAsync(medewerker.AccessTokenId),
        Voornaam = medewerker.Voornaam, Achternaam = medewerker.Achternaam, Email = medewerker.Email, Afbeelding = medewerker.Afbeelding, GeboorteDatum = medewerker.GeboorteDatum, RolNaam = medewerker.RolNaam};
        return medewerkerInfo;
    }

    [HttpPost("use2fa")]
    public async Task<ActionResult> Use2FA([FromBody] AccessTokenKey accessTokenKey)
    { //Nog fixen dat hij deze 2 params accepteert.
        Medewerker m = await GetMedewerkerByAccessToken(accessTokenKey.AccessToken);
        if (m == null) HandleResponse("UserNotFoundError");
        var responseString = await _service.Use2FA(m, accessTokenKey.Key);
        if (responseString == "Success")
        {
            m.TwoFactorAuthSetupComplete = true;
            await _context.SaveChangesAsync();
        }
        return HandleResponse(responseString);
    }

    [HttpPost("logoutall")]
    public async Task<ActionResult> LogoutAll([FromBody] AccessTokenObject accessTokenObject)
    {
        Medewerker medewerker = await GetMedewerkerByAccessToken(accessTokenObject.AccessToken);
        if (medewerker == null) return HandleResponse("Error");
        AccessToken accessToken = await _context.AccessTokens.FirstOrDefaultAsync(a => a.Token == accessTokenObject.AccessToken);
        if (accessToken == null) return HandleResponse("Error");
        _context.AccessTokens.Remove(accessToken);
        medewerker.AccessToken = null;
        medewerker.AccessTokenId = null;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("request/passwordreset/{email}")]
    public async Task<ActionResult> InitiatePasswordReset(string email){
        Medewerker medewerker = await GetMedewerkerByEmailAsync(email);
        if(medewerker == null) return BadRequest();
        return HandleResponse(await _service.InitiatePasswordReset(medewerker, _context));
    }

    [HttpPost("complete/passwordreset/{email}")]
    public async Task<ActionResult> CompletePasswordReset([FromBody] AuthenticatieTokenNieuwWachtwoord authenticatieTokenNieuwWachtwoord, string email){
        Medewerker medewerker = await GetMedewerkerByEmailAsync(email);
        if(medewerker == null) return BadRequest();
        return HandleResponse(await _service.ResetPassword(medewerker, authenticatieTokenNieuwWachtwoord.AuthenticatieToken, authenticatieTokenNieuwWachtwoord.NieuwWachtwoord, _context));
    } 
    
    [HttpPost("rol/by/at")]
    public async Task<ActionResult<string>> GetRolByAT([FromBody] AccessTokenObject accessTokenObject){
        Medewerker medewerker = await GetMedewerkerByAccessToken(accessTokenObject.AccessToken);
        string rol = medewerker.RolNaam;
        return rol;
    }

    // FUNCTIONS

    public async Task<Medewerker> GetMedewerkerByAccessToken(string AccessToken)
    {
        AccessToken accessToken = await _context.AccessTokens.FirstOrDefaultAsync(a => a.Token == AccessToken);
        if (accessToken == null) return null;
        Medewerker m = await _context.Medewerkers.FirstOrDefaultAsync(m => m.AccessToken == accessToken);
        if (m == null) return null; // error message weghalen, is voor debugging.
        else if (accessToken.VerloopDatum < DateTime.Now) return null;
        return m;
    }

    public async Task<AccessToken> GetAccessTokenByTokenIdAsync(string AccessTokenId)
    {
        AccessToken accessToken = await _context.AccessTokens.FirstOrDefaultAsync(a => a.Token == AccessTokenId);
        return accessToken;
    }

    public ActionResult HandleResponse(string response)
    {
        var responses = ResponseList.Responses;
        if (responses.ContainsKey(response))
        {
            return StatusCode(responses[response].StatusCode, responses[response].Message);
        }
        return StatusCode(500);
    }

    public async Task<Medewerker> GetMedewerkerByEmailAsync(string email)
    {
        Medewerker m = await _context.Medewerkers.FirstOrDefaultAsync(m => m.Email == email);
        return m;
    }
}


public class MedewerkerInfo
{
    public bool TwoFactorAuthSetupComplete { set; get; }
    public bool IsBlocked { set; get; }
    public AccessToken AccessToken { set; get; }
    public string Voornaam {set; get;}
    public string Achternaam {set; get;}
    public string Email {set; get;}
    public string Afbeelding {set; get;}
    public DateTime GeboorteDatum {set; get;}
    public string RolNaam {set; get;}
}