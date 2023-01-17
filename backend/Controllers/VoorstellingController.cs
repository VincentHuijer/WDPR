using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VoorstellingController : ControllerBase
{
    private PrintBestelling _printBestelling = new PrintBestelling();
    private readonly GebruikerContext _context;
    private Kalender _kalender = new Kalender();
    public VoorstellingController(GebruikerContext context)
    {
        _context = context;
    }

    //dit is alleen om het printen van tickets te testen
    [HttpGet("Gettest")]
    public async Task<string> Gettest(Bestelling bestellinginformatie)
    {
        return _printBestelling.ticketPrinten(bestellinginformatie);
    }

    [HttpGet("GetVoorstellingen/leeftijd/{age}")]
    public async Task<List<Voorstelling>> GetVoorstellingen(int age, [FromQuery] string sortOrder = "ascending")
    {
        List<Voorstelling> voorstellingen = await _context.Voorstellingen
                                           .Where(v => v.leeftijd <= age)
                                           .ToListAsync();

        if (sortOrder == "ascending")
        {
            voorstellingen = voorstellingen.OrderBy(v => v.leeftijd).ToList();
        }
        else if (sortOrder == "descending")
        {
            voorstellingen = voorstellingen.OrderByDescending(v => v.leeftijd).ToList();
        }
        return voorstellingen;
    }

    [HttpGet("getvoorstellingen/order")]
    public async Task<List<Voorstelling>> GetVoorstellingen([FromQuery] string order){
        List<Voorstelling> voorstellingen = await _context.Voorstellingen.ToListAsync();
        if(voorstellingen.Count() == 0) return voorstellingen;
        if(order == "leeftijd") voorstellingen = voorstellingen.OrderBy(v => v.leeftijd).ToList();
        else if(order == "prijs"){
        List<KeyValuePair<double, Voorstelling>> VoorstellingPair = new List<KeyValuePair<double, Voorstelling>>();
            foreach(var voorstelling in voorstellingen){
                List<Show> shows = await _context.Shows.Where(s => s.VoorstellingId == voorstelling.VoorstellingId).ToListAsync();
                double lowest = 999;
                foreach(var show in shows){
                    double prijs = await _context.Stoelen.Where(s => s.Zaalnummer == show.Zaalnummer).MinAsync(s => s.Prijs);
                    if(prijs < lowest) lowest = prijs;
                }
                VoorstellingPair.Add(new KeyValuePair<double, Voorstelling>(lowest, voorstelling));
            }
            VoorstellingPair.OrderBy(v => v.Key);
            List<Voorstelling> toReturn = new List<Voorstelling>();
            foreach(var item in VoorstellingPair) toReturn.Add(item.Value);
            voorstellingen = toReturn;
            voorstellingen.Reverse();
        }
        return voorstellingen;
    }

    [HttpGet("GetVoorstellingen")]
    public async Task<List<Voorstelling>> GetVoorstellingen()
    {
        List<Voorstelling> voorstellingen = await _context.Voorstellingen.ToListAsync();
        return voorstellingen;
    }

    [HttpGet("GetVoorstellingWithId/{id}")]
    public async Task<ActionResult<Voorstelling>> GetVoorstellingWithId(int id)
    {
        Voorstelling v = await _context.Voorstellingen.FirstOrDefaultAsync(v => v.VoorstellingId == id);
        if(v == null){
            return NotFound();
        }

        return v;
    }

    [HttpPost("AddVoorstelling")]
    public async Task<ActionResult> AddVoorstelling([FromBody] NieuweVoorstelling nieuweVoorstelling)
    {
        _kalender = _context.Kalenders.Find(0);
        Console.WriteLine("parameters:" + nieuweVoorstelling.Titel + nieuweVoorstelling.Omschrijving);
        Voorstelling voorstelling = new Voorstelling(nieuweVoorstelling.Titel, nieuweVoorstelling.Omschrijving, nieuweVoorstelling.Image);
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

public class VoorstellingIdObject
{
    public int Id { get; set; }
}

public class NieuweVoorstelling
{
    public string Titel { get; set; }
    public string Omschrijving { get; set; }
    public string Image { get; set; }
}
