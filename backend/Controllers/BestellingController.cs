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

    [HttpPost("nieuwebestelling")]
    public async Task<ActionResult<string>> NieuweBestelling([FromBody] BestelInfo bestelInfo){
        Klant klant = await _context.Klanten.FirstOrDefaultAsync(k => k.AccessTokenId == bestelInfo.AccessToken);
        if(klant == null) return NotFound();
        Show show = _context.Shows.FirstOrDefault(s => s.ShowId == bestelInfo.ShowId);
        if(show == null) return NotFound();
        List<Stoel> Stoelen = new List<Stoel>();
        int Totaalbedrag = 0;

        foreach(var StoelId in bestelInfo.StoelIds){
            Stoel stoel = _context.Stoelen.FirstOrDefault(s => s.StoelID == StoelId);
            Totaalbedrag += (int) stoel.Prijs;
            Stoelen.Add(stoel);
            if(await _context.BesteldeStoelen.Where(s => s.Datum == show.Datum).AnyAsync(s => s.StoelID == stoel.StoelID)) return StatusCode(403, "Stoel bezet");
        }

        Bestelling bestelling = new Bestelling(){Totaalbedrag = Totaalbedrag, BestelDatum = DateTime.Now, isBetaald = false, kortingscode = 0};
        await _context.AddAsync(bestelling);
        await _context.SaveChangesAsync();
        foreach(var Stoel in Stoelen){
            BesteldeStoel besteldeStoel = new BesteldeStoel(){Bestelling = bestelling, BestellingId = bestelling.BestellingId, Stoel = Stoel, StoelID = Stoel.StoelID, Datum = show.Datum};
            await _context.AddAsync(besteldeStoel);
        }
        await _context.SaveChangesAsync();
        return bestelling.BestellingId.ToString();
    }
 

    // [HttpPost("AddBestelling")]
    // public async Task<ActionResult> AddBestelling([FromBody] BestellingBody bestellingBody)
    // {
    //     DateTime bestelDatum = DateTime.Parse(bestellingBody.BestelDatum);
    //     Bestelling bestelling = new Bestelling() { BestelDatum = bestelDatum, Totaalbedrag = bestellingBody.Totaalbedrag };

    //     foreach (var item in bestellingBody.Stoelen)
    //     {
    //         Stoel stoel = await _context.Stoelen.FirstOrDefaultAsync(v => v.StoelID.ToString() == item);
    //         BesteldeStoel besteldeStoel = new BesteldeStoel() { Bestelling = bestelling, BestellingId = bestelling.BestellingId, Stoel = stoel, StoelID = stoel.StoelID, Datum = bestelDatum };

    //         _context.BesteldeStoelen.Add(besteldeStoel);
    //     }

    //     _context.Bestellingen.Add(bestelling);

    //     await _context.SaveChangesAsync();

    //     return Ok();
    // }

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

public class BestelInfo{
    public int ShowId {set; get;}
    public List<int> StoelIds {set; get;}
    public string AccessToken {set; get;}
}