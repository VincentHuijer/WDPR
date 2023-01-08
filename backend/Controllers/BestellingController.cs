using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BestellingController : ControllerBase
{
    private readonly GebruikerContext _context;
    public BestellingController(GebruikerContext context)
    {
        _context = context;
    }


    [HttpGet("getBestelling")]
    public async Task<List<Bestelling>> GetBestellingen()
    {
        List<Bestelling> bestellingen = await _context.Bestelling.ToListAsync();
        return bestellingen;
    }

    [HttpPost("AddBestelling")]
    public async Task<ActionResult> AddBestelling(Bestelling bestelling)
    {

        _context.Bestelling.Add(bestelling);
        if (await _context.SaveChangesAsync() > 0)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost("VerwijderBestelling")]
    public async Task<ActionResult> VerwijderBestelling(Bestelling bestelling)
    {
        _context.Bestelling.Remove(bestelling);
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