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

    [HttpPost("get/{id}")]
    public async Task<ActionResult<ArtiestGroep>> GetGroepById([FromBody] AccessTokenObject accessToken, int id){
        if(!await IsAllowed(accessToken, "Medewerker", true)) return StatusCode(403, "No permissions!");
        ArtiestGroep groep = await _context.ArtiestGroepen.FirstOrDefaultAsync(ag => ag.GroepsId == id);
        if(groep == null) return NotFound();
        return groep;
    }

    [HttpPost("groep/by/at")]
    public async Task<ActionResult<ArtiestGroep>> GetGroepByUser([FromBody] AccessTokenObject accessToken){
        if(!await IsAllowed(accessToken, "Artiest", false) && !await IsAllowed(accessToken, "Medewerker", true)) return StatusCode(403, "No permissions!");
        Klant klant = await GetKlantByAccessToken(accessToken.AccessToken);
        if(klant == null) return NotFound();
        ArtiestGroep artiestGroep = await _context.ArtiestGroepen.FirstOrDefaultAsync(a => a.GroepsId == klant.ArtiestGroepId);
        if(artiestGroep == null) return NotFound();
        return artiestGroep;
    }

    [HttpPost("get/{id}/leden")]
    public async Task<ActionResult<List<KlantInfoShort>>> GetLeden([FromBody] AccessTokenObject accessToken, int id){
        if(!await IsAllowed(accessToken, "Artiest", false) && !await IsAllowed(accessToken, "Medewerker", true)) return StatusCode(403, "No permissions!");
        ArtiestGroep groep = await _context.ArtiestGroepen.FirstOrDefaultAsync(a => a.GroepsId == id);
        if (groep == null) return NotFound();
        List<Klant> groepsleden = await _context.Klanten.Where(k => k.ArtiestGroepId == groep.GroepsId).ToListAsync();
        List<KlantInfoShort> groepsledeninfo = new List<KlantInfoShort>();
        foreach(var groepslid in groepsleden){
            groepsledeninfo.Add(new KlantInfoShort(){Voornaam = groepslid.Voornaam, Achternaam = groepslid.Achternaam != null? groepslid.Achternaam : "N/A", Email = groepslid.Email});
        }
        return groepsledeninfo;
    }

    [HttpPost("groepen")]
    public async Task<ActionResult<List<ArtiestGroep>>> GetGroepen([FromBody] AccessTokenObject accessToken){
        if(!await IsAllowed(accessToken, "Medewerker", true)) return StatusCode(403, "No permissions!");
        return await _context.ArtiestGroepen.ToListAsync();
    }

    [HttpPost("nieuwegroep")]
    public async Task<ActionResult> CreateGroep([FromBody] NieuweGroep nieuweGroep){
        if(!await IsAllowed(new AccessTokenObject(){AccessToken = nieuweGroep.AccessToken}, "Medewerker", true)) return StatusCode(403, "No permissions!");
        ArtiestGroep artiestGroep = new ArtiestGroep(){Groepsnaam = nieuweGroep.Groepsnaam, Omschrijving = nieuweGroep.Omschrijving};
        await _context.AddAsync(artiestGroep);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("verwijdergroep/{groepsId}")]
    public async Task<ActionResult> RemoveGroep([FromBody] AccessTokenObject accessToken, int groepsId){
        if(!await IsAllowed(accessToken, "Medewerker", true)) return StatusCode(403, "No permissions!");
        ArtiestGroep artiestGroep = await _context.ArtiestGroepen.FirstOrDefaultAsync(ag => ag.GroepsId == groepsId);
        if(artiestGroep == null) return NotFound();
        _context.ArtiestGroepen.Remove(artiestGroep);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("{groepsId}/nieuwlid/{klantId}")]
    public async Task<ActionResult> AddLid([FromBody] AccessTokenObject accessToken, int groepsId, int klantId){
        if(!await IsAllowed(accessToken, "Medewerker", true)) return StatusCode(403, "No permissions!");
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
        if(!await IsAllowed(accessToken, "Medewerker", true)) return StatusCode(403, "No permissions!");
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
    public async Task<bool> IsAllowed(AccessTokenObject AccessToken, string BenodigdeRol, bool isMedewerker){
        if(isMedewerker){
            Medewerker medewerker = await GetMedewerkerByAccessToken(AccessToken.AccessToken);
            if(medewerker == null) return false;
            if(medewerker.RolNaam == BenodigdeRol) return true;
            return false;
        }else{
            Klant klant = await GetKlantByAccessToken(AccessToken.AccessToken);
            if(klant == null) return false;
            if(klant.RolNaam == BenodigdeRol) return true;
            return false;
        }
    }
    public async Task<Klant> GetKlantByAccessToken(string AccessToken){
        AccessToken accessToken = await _context.AccessTokens.FirstOrDefaultAsync(a => a.Token == AccessToken);
        if(accessToken == null) return null;
        Klant k = await _context.Klanten.FirstOrDefaultAsync(k => k.AccessToken == accessToken);
        if(k == null) return null; 
        else if(accessToken.VerloopDatum < DateTime.Now) return null;
        return k;
    }
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
