using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VoorstellingController : ControllerBase
{
    //nog ff maken

    private readonly GebruikerContext _context;
    private Kalender _kalender = new Kalender();
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
    public async Task<ActionResult> AddVoorstelling([FromBody] NieuweVoorstelling nieuweVoorstelling)
    {
        _kalender = _context.Kalenders.Find(0);
        Console.WriteLine("parameters:" +  nieuweVoorstelling.Titel + nieuweVoorstelling.Zaalnummer + nieuweVoorstelling.Omschrijving + nieuweVoorstelling.Prijs + nieuweVoorstelling.Datum);
        Voorstelling voorstelling = new Voorstelling(nieuweVoorstelling.Titel, nieuweVoorstelling.Zaalnummer, nieuweVoorstelling.Omschrijving, nieuweVoorstelling.Prijs, nieuweVoorstelling.Datum, nieuweVoorstelling.Image);
        //interval = "once", "weekly","monthly","yearly"
        //aantalKeer = aantal keer dat de afspraak herhaalt wordt
        //interval is weekly en aantalKeer is 5, dan wordt de afspraak elke week herhaalt voor 5 weken

        // List<Voorstelling> voorlijst = new List<Voorstelling>();
        // voorlijst = _kalender.HerhaalOptie(nieuweVoorstelling.Interval, nieuweVoorstelling.AantalKeer, voorstelling);
        // for (int i = 0; i < voorlijst.Count; i++)
        // {
        _context.Voorstellingen.Add(voorstelling);
        // }
        if (await _context.SaveChangesAsync() > 0)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost("VerwijderVoorstelling")]
    public async Task<ActionResult> VerwijderVoorstelling([FromBody] Voorstelling voorstelling)
    {
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

public class NieuweVoorstelling
{
    public string Titel { get; set; }
    public string Omschrijving { get; set; }
    public int Zaalnummer { get; set; }
    public double Prijs { get; set; }
    public DateTime Datum { get; set; }
    public string Image { get; set; }
}
