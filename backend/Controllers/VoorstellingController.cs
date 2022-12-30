using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VoorstellingController : ControllerBase
{
    //nog ff maken

    private readonly GebruikerContext _context;
    private Kalender _kalender;

    public VoorstellingController(GebruikerContext context)
    {
        _context = context;
    }


    [HttpGet("GetVoorstellingen")]
    public async Task<List<Voorstelling>> GetVoorstellingen()
    {
        List<Voorstelling> voorstellingen = await _context.Voorstellingen.ToListAsync();
        return voorstellingen;
    }

    [HttpPost("AddVoorstelling")]
    public async Task<ActionResult> AddVoorstelling([FromQuery] string interval, int aantalKeer, Voorstelling voor)
    {
        //interval = "once", "weekly","monthly","yearly"
        //aantalKeer = aantal keer dat de afspraak herhaalt wordt
        // interval is weekly en aantalKeer is 5, dan wordt de afspraak elke week herhaalt voor 5 weken
        _kalender.HerhaalOptie(interval, aantalKeer, voor);
        _context.Voorstellingen.Add(voor);
        if(await _context.SaveChangesAsync() > 0)
        {
            return Ok();
        }        else
        {
            return BadRequest();
        }
    }

    [HttpPost("VerwijderVoorstelling")]
    public async Task<ActionResult> VerwijderVoorstelling([FromBody] Voorstelling voorstelling)
    {
        _context.Voorstellingen.Remove(voorstelling);
        if(await _context.SaveChangesAsync() > 0)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

}
