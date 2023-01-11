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
    public async Task<ActionResult<ArtiestGroep>> GetGroepById(int id){
        ArtiestGroep groep = await _context.ArtiestGroepen.FirstOrDefaultAsync(ag => ag.GroepsId == id);
        if(groep == null) return NotFound();
        return groep;
    }

    [HttpGet("groepen")]
    public async Task<ActionResult<List<ArtiestGroep>>> GetGroepen(){
        return await _context.ArtiestGroepen.ToListAsync();
    }

    [HttpPost("nieuwegroep")]
    public async Task<ActionResult> CreateGroep([FromBody] NieuweGroep nieuweGroep){
        ArtiestGroep artiestGroep = new ArtiestGroep(){Groepsnaam = nieuweGroep.Groepsnaam, Omschrijving = nieuweGroep.Omschrijving};
        await _context.AddAsync(artiestGroep);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("{groepsId}/nieuwlid/{klantId}")]
    public async Task<ActionResult> AddLid(int groepsId, int klantId){
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
    public async Task<ActionResult> RemoveLid(int groepsId, int klantId){
        ArtiestGroep artiestGroep = await _context.ArtiestGroepen.FirstOrDefaultAsync(ag => ag.GroepsId == groepsId);
        if(artiestGroep == null) return NotFound();
        Klant klant = await _context.Klanten.FirstOrDefaultAsync(k => k.Id == klantId);
        if(klant == null) return NotFound();
        klant.ArtiestGroep = null;
        klant.ArtiestGroepId = null;
        await _context.SaveChangesAsync();
        return Ok();
    }
}

public class NieuweGroep{
    public string Groepsnaam {set; get;}
    public string Omschrijving {set; get;}
}