using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroepController : ControllerBase{
    private readonly GebruikerContext _context;
    public GroepController(GebruikerContext context)
    {
        _context = context;
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<ArtiestGroep>> GetGroepById([FromBody] AccessTokenObject accessToken, int id){
        if(!await IsAllowed(accessToken, "Medewerker")) return StatusCode(403, "No permissions!");
        ArtiestGroep groep = await _context.ArtiestGroepen.FirstOrDefaultAsync(ag => ag.GroepsId == id);
        if(groep == null) return NotFound();
        return groep;
    }

    [HttpGet("groepen")]
    public async Task<ActionResult<List<ArtiestGroep>>> GetGroepen([FromBody] AccessTokenObject accessToken){
        if(!await IsAllowed(accessToken, "Medewerker")) return StatusCode(403, "No permissions!");
        return await _context.ArtiestGroepen.ToListAsync();
    }

    [HttpPost("nieuwegroep")]
    public async Task<ActionResult> CreateGroep([FromBody] NieuweGroep nieuweGroep){
        if(!await IsAllowed(new AccessTokenObject(){AccessToken = nieuweGroep.AccessToken}, "Medewerker")) return StatusCode(403, "No permissions!");
        ArtiestGroep artiestGroep = new ArtiestGroep(){Groepsnaam = nieuweGroep.Groepsnaam, Omschrijving = nieuweGroep.Omschrijving};
        await _context.AddAsync(artiestGroep);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("{groepsId}/nieuwlid/{klantId}")]
    public async Task<ActionResult> AddLid([FromBody] AccessTokenObject accessToken, int groepsId, int klantId){
        if(!await IsAllowed(accessToken, "Medewerker")) return StatusCode(403, "No permissions!");
        ArtiestGroep artiestGroep = await _context.ArtiestGroepen.FirstOrDefaultAsync(ag => ag.GroepsId == groepsId);
        if(artiestGroep == null) return NotFound();
        Klant klant = await _context.Klanten.FirstOrDefaultAsync(k => k.Id == klantId);
        if(klant == null) return NotFound();
        klant.ArtiestGroep = artiestGroep;
        klant.ArtiestGroepId = groepsId;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("{groepsId}/verwijderlid/{klantId}")]
    public async Task<ActionResult> RemoveLid([FromBody] AccessTokenObject accessToken, int groepsId, int klantId){
        if(!await IsAllowed(accessToken, "Medewerker")) return StatusCode(403, "No permissions!");
        ArtiestGroep artiestGroep = await _context.ArtiestGroepen.FirstOrDefaultAsync(ag => ag.GroepsId == groepsId);
        if(artiestGroep == null) return NotFound();
        Klant klant = await _context.Klanten.FirstOrDefaultAsync(k => k.Id == klantId);
        if(klant == null) return NotFound();
        klant.ArtiestGroep = null;
        klant.ArtiestGroepId = null;
        await _context.SaveChangesAsync();
        return Ok();
    }
    public async Task<Medewerker> GetMedewerkerByAccessToken(string AccessToken)
    {
        AccessToken accessToken = await _context.AccessTokens.FirstOrDefaultAsync(a => a.Token == AccessToken);
        if (accessToken == null) return null;
        Medewerker m = await _context.Medewerkers.FirstOrDefaultAsync(m => m.AccessToken == accessToken);
        if (m == null) return null; // error message weghalen, is voor debugging.
        else if (accessToken.VerloopDatum < DateTime.Now) return null;
        return m;
    }
    public async Task<bool> IsAllowed(AccessTokenObject AccessToken, string BenodigdeRol){
        Medewerker medewerker = await GetMedewerkerByAccessToken(AccessToken.AccessToken);
        if(medewerker == null) return false;
        if(medewerker.RolNaam == BenodigdeRol) return true;
        return false;
    }
}

public class NieuweGroep{
    public string Groepsnaam {set; get;}
    public string Omschrijving {set; get;}
    public string AccessToken {set; get;}
}
