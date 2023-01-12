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
        List<Bestelling> bestellingen = await _context.Bestellingen.ToListAsync();
        return bestellingen;
    }

    [HttpPost("AddBestelling")]
    public async Task<ActionResult> AddBestelling([FromBody] BestellingBody bestellingBody)
    {
        DateTime bestelDatum = DateTime.Parse(bestellingBody.BestelDatum);
        Bestelling bestelling = new Bestelling() { BestelDatum = bestelDatum, Totaalbedrag = bestellingBody.Totaalbedrag };

        foreach (var item in bestellingBody.Stoelen)
        {
            Stoel stoel = await _context.Stoelen.FirstOrDefaultAsync(v => v.StoelID.ToString() == item);
            BesteldeStoel besteldeStoel = new BesteldeStoel() { Bestelling = bestelling, BestellingId = bestelling.BestellingId, Stoel = stoel, StoelID = stoel.StoelID, Datum = bestelDatum };

            _context.BesteldeStoelen.Add(besteldeStoel);
        }

        _context.Bestellingen.Add(bestelling);

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("VerwijderBestelling")]
    public async Task<ActionResult> VerwijderBestelling(Bestelling bestelling)
    {
        _context.Bestellingen.Remove(bestelling);
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

public class BestellingBody
{
    public int Totaalbedrag { get; set; }
    public string BestelDatum { get; set; }

    public double Kortingscode { get; set; }

    public List<string> Stoelen { get; set; }
}