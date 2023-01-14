using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]


public class BetalingController : ControllerBase
{
    private readonly GebruikerContext _context;
    public BetalingController(GebruikerContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> Betaling([FromForm] Betaling betaling)
    {
        Bestelling bestelling = await _context.Bestellingen.FirstOrDefaultAsync(b => b.BestellingId == betaling.reference);
        if (bestelling == null) return NotFound();
        bestelling.isBetaald = betaling.isBetaald;
        await _context.SaveChangesAsync();
        return Ok();
    }


    [HttpGet("bestelling/{id}")]
    public async Task<ActionResult<Bestelling>> GetBestelling(int id)
    {
        Bestelling bestelling = await _context.Bestellingen.FirstOrDefaultAsync(b => b.BestellingId == id);
        if (bestelling == null) return NotFound();
        return bestelling;
    }

}

public class Betaling
{
    public bool isBetaald { get; set; }
    public int reference { get; set; }
}