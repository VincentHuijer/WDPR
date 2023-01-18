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
}
