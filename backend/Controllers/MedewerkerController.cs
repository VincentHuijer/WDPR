using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedewerkerController : ControllerBase
{
    private readonly GebruikerContext _context;
    private readonly IPermissionService _permissionService = new PermissionService();

    private IMedewerkerService _service = new MedewerkerService();

    public MedewerkerController(GebruikerContext context)
    {
        _context = context;
    }

    // ENDPOINTS
    [HttpPost("login")] //DONE
    public async Task<ActionResult<MedewerkerInfo>> LoginMedewerker([FromBody] EmailWachtwoord emailWachtwoord)
    {
        var responseString = await _service.Login(emailWachtwoord.Email, emailWachtwoord.Wachtwoord, _context);
        if (responseString == "Success")
        {
            Medewerker medewerker = await _permissionService.GetMedewerkerByEmailAsync(emailWachtwoord.Email, _context);
            if (medewerker == null) return HandleResponse("Error");
            if (medewerker.AccessTokenId == null)
            {
                medewerker.AccessToken = new AccessToken() { Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(7) };
                await _context.SaveChangesAsync();
            }

            MedewerkerInfo medewerkerInfo = new MedewerkerInfo()
            {
                Id=medewerker.Id,
                TwoFactorAuthSetupComplete = medewerker.TwoFactorAuthSetupComplete,
                IsBlocked = medewerker.IsBlocked,
                AccessToken = await _permissionService.GetAccessTokenByTokenIdAsync(medewerker.AccessTokenId, _context),
                Achternaam = medewerker.Email,
                Voornaam = medewerker.Voornaam,
                Email = medewerker.Email,
                RolNaam = medewerker.RolNaam
            };
            return medewerkerInfo;
        }

        var response = HandleResponse(responseString);
        return response;
    }

    [HttpPost("setup2fa")] //DONE
    public async Task<ActionResult<List<string>>> Setup2FA([FromBody] AccessTokenObject AccessToken) 
    {

        Medewerker m = await _permissionService.GetMedewerkerByAccessToken(AccessToken.AccessToken, _context);
        if (m == null) HandleResponse("UserNotFoundError");
        var res = await _service.Setup2FA(m, _context);
        if (res.Item1 == "" && res.Item2 == "") return HandleResponse("AlreadySetup2FA");
        List<string> responses = new List<string>();
        responses.Add(res.Item1);
        responses.Add(res.Item2);
        return responses;
    }

    [HttpPost("medewerker/by/at")] //DONE
    public async Task<ActionResult<MedewerkerInfo>> GetKlantInfoByAT([FromBody] AccessTokenObject accessTokenObject)
    {
        Medewerker medewerker = await _permissionService.GetMedewerkerByAccessToken(accessTokenObject.AccessToken, _context);
        MedewerkerInfo medewerkerInfo = new MedewerkerInfo()
        {
            Id = medewerker.Id,
            TwoFactorAuthSetupComplete = medewerker.TwoFactorAuthSetupComplete,
            IsBlocked = medewerker.IsBlocked,
            AccessToken = await _permissionService.GetAccessTokenByTokenIdAsync(medewerker.AccessTokenId, _context),
            Voornaam = medewerker.Voornaam,
            Achternaam = medewerker.Achternaam,
            Email = medewerker.Email,
            Afbeelding = medewerker.Afbeelding,
            GeboorteDatum = medewerker.GeboorteDatum,
            RolNaam = medewerker.RolNaam
        };
        return medewerkerInfo;
    }

    [HttpPost("use2fa")] //DONE
    public async Task<ActionResult> Use2FA([FromBody] AccessTokenKey accessTokenKey)
    { 
        Medewerker m = await _permissionService.GetMedewerkerByAccessToken(accessTokenKey.AccessToken, _context);
        if (m == null) HandleResponse("UserNotFoundError");
        var responseString = await _service.Use2FA(m, accessTokenKey.Key);
        if (responseString == "Success")
        {
            m.TwoFactorAuthSetupComplete = true;
            await _context.SaveChangesAsync();
        }
        return HandleResponse(responseString);
    }

    [HttpPost("logoutall")] //DONE
    public async Task<ActionResult> LogoutAll([FromBody] AccessTokenObject accessTokenObject)
    {
        Medewerker medewerker = await _permissionService.GetMedewerkerByAccessToken(accessTokenObject.AccessToken, _context);
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
    public async Task<ActionResult> InitiatePasswordReset(string email)
    {
        Medewerker medewerker = await _permissionService.GetMedewerkerByEmailAsync(email, _context);
        if (medewerker == null) return BadRequest();
        return HandleResponse(await _service.InitiatePasswordReset(medewerker, _context));
    }

    [HttpPost("complete/passwordreset/{email}")]
    public async Task<ActionResult> CompletePasswordReset([FromBody] AuthenticatieTokenNieuwWachtwoord authenticatieTokenNieuwWachtwoord, string email)
    {
        Medewerker medewerker = await _permissionService.GetMedewerkerByEmailAsync(email, _context);
        if (medewerker == null) return BadRequest();
        return HandleResponse(await _service.ResetPassword(medewerker, authenticatieTokenNieuwWachtwoord.AuthenticatieToken, authenticatieTokenNieuwWachtwoord.NieuwWachtwoord, _context));
    }

    // [HttpPost("rol/by/at")]
    // public async Task<ActionResult<string>> GetRolByAT([FromBody] AccessTokenObject accessTokenObject)
    // {
    //     Medewerker medewerker = await _permissionService.GetMedewerkerByAccessToken(accessTokenObject.AccessToken, _context);
    //     string rol = medewerker.RolNaam;
    //     return rol;
    // }

    // FUNCTIONS


    public ActionResult HandleResponse(string response)
    {
        var responses = ResponseList.Responses;
        if (responses.ContainsKey(response))
        {
            return StatusCode(responses[response].StatusCode, responses[response].Message);
        }
        return StatusCode(500);
    }



        [HttpPost("AddMedewerker")]
        public async Task<ActionResult> AddMedewerker([FromBody] nieuweMedewerker nieuweMedewerker)
        {
            if(!await _permissionService.IsAllowed(new AccessTokenObject(){AccessToken = nieuweMedewerker.AccessToken}, "Admin", true, _context)) return StatusCode(403, "No permissions!");
            Medewerker medewerker = new Medewerker(nieuweMedewerker.Voornaam, nieuweMedewerker.Achternaam, nieuweMedewerker.Email, nieuweMedewerker.Wachtwoord);
            medewerker.RolNaam = "Medewerker";
            _context.Medewerkers.Add(medewerker);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("VerwijderMedewerker")]
        public async Task<ActionResult> VerwijderMedewerker([FromBody] AccessId accessId)
        {
            if(!await _permissionService.IsAllowed(new AccessTokenObject(){AccessToken = accessId.AccessToken}, "Admin", true, _context)) return StatusCode(403, "No permissions!");
            Medewerker medewerker = _context.Medewerkers.FirstOrDefault(m => m.Id == accessId.Id);
            if (medewerker == null) return BadRequest();
            _context.Medewerkers.Remove(medewerker);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("getmedewerkers")]
        public async Task<ActionResult<List<MedewerkerInfo>>> GetMedewerkers([FromBody] AccessTokenObject accessToken){
            if(!await _permissionService.IsAllowed(accessToken, "Admin", true, _context)) return StatusCode(403, "No permissions!");
            List<Medewerker> medewerkers = await _context.Medewerkers.ToListAsync();
            List<MedewerkerInfo> toReturn = new List<MedewerkerInfo>();
            foreach(var medewerker in medewerkers){
                toReturn.Add(new MedewerkerInfo(){
                    Id = medewerker.Id,
                    TwoFactorAuthSetupComplete = medewerker.TwoFactorAuthSetupComplete,
                    IsBlocked = medewerker.IsBlocked,
                    AccessToken = await _permissionService.GetAccessTokenByTokenIdAsync(medewerker.AccessTokenId, _context),
                    Voornaam = medewerker.Voornaam,
                    Achternaam = medewerker.Achternaam,
                    Email = medewerker.Email,
                    Afbeelding = medewerker.Afbeelding,
                    GeboorteDatum = medewerker.GeboorteDatum,
                    RolNaam = medewerker.RolNaam
                });
            }
            return toReturn;
        }

        // [HttpPost("UpdateMedewerkerRole/{id}/{rolNaam}")]
        // public async Task<ActionResult> UpdateMedewerkerRol(int id, string rolNaam)
        // {
        //     Medewerker medewerker = _context.Medewerkers.FirstOrDefault(m => m.Id == id);
        //     if (medewerker == null) return BadRequest();
        //     Rol rol = new Rol();
        //     medewerker.Rol = rol.CheckRol(rolNaam);
        //     medewerker.RolNaam = rolNaam;
        //     if (medewerker.Rol.ToString() != rolNaam) return BadRequest();
        //     if (await _context.SaveChangesAsync() > 0)
        //     {
        //         return Ok();
        //     }
        //     else
        //     {
        //         return BadRequest();
        //     }
        // }
    }

