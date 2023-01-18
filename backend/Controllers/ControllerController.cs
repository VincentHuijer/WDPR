using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ControllerController : ControllerBase
{
    private PrintBestelling _printBestelling = new PrintBestelling();
    private readonly GebruikerContext _context;
    private Kalender _kalender = new Kalender();
    public ControllerController(GebruikerContext context)
    {
        _context = context;
        _kalender = _context.Kalenders.Find(0);
    }

    [HttpPost("AddVoorstelling")]
    public async Task<ActionResult> AddVoorstelling([FromBody] NieuweVoorstelling nieuweVoorstelling)
    {
        
        Voorstelling voorstelling = new Voorstelling(nieuweVoorstelling.Titel, nieuweVoorstelling.Omschrijving, nieuweVoorstelling.Image);
        _context.Voorstellingen.Add(voorstelling);
        if (await _context.SaveChangesAsync() > 0)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost("VerwijderVoorstelling/{id}")]
    public async Task<ActionResult> VerwijderVoorstellingDatum(int id)
    {
        Voorstelling voorstelling = _context.Voorstellingen.Find(id);
        _context.Voorstellingen.Remove(voorstelling);
        if (await _context.SaveChangesAsync() > 0)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    // [HttpPost("AddShow")]
    // public async Task<ActionResult> AddShow([FromBody] HerhaalbareShow HerhaalShow)
    // {
    //     Show show = new Show(HerhaalShow.Zaalnummer, HerhaalShow.StartDatum, HerhaalShow.VoorstellingId, _kalender.KalenderId);
        
    //     _context.Shows.Add(show);
    //     List<Show> showlist = _kalender.HerhaalOptie(HerhaalShow.Interval, HerhaalShow.AantalKeer, show);
    //     foreach (Show s in showlist)
    //     {
    //         _context.Shows.Add(s);
    //     }

    //     if (await _context.SaveChangesAsync() > 0)
    //     {
    //         return Ok();
    //     }
    //     else
    //     {
    //         return BadRequest();
    //     }
    // }

    // [HttpPost("VerwijderShow/{id}")]
    // public async Task<ActionResult> VerwijderShow(int id)
    // {
    //     Show show = _context.Shows.Find(id);
    //     _context.Shows.Remove(show);
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
