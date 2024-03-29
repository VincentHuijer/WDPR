using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VoorstellingController : ControllerBase
{
    private readonly GebruikerContext _context;
    private readonly IPermissionService _permissionService = new PermissionService();
    private Kalender _kalender = new Kalender();
    public VoorstellingController(GebruikerContext context)
    {
        _context = context;
    }


    [HttpGet("getvoorstellingen/leeftijd/{age}")] //DONE
    public async Task<ActionResult<List<Voorstelling>>> GetVoorstellingenByAge(int age, [FromQuery] string? order)
    {
        List<Voorstelling> voorstellingen = await _context.Voorstellingen
                                           .Where(v => v.leeftijd <= age)
                                           .ToListAsync();

        if(order == null || (order.ToLower() != "prijs" && order.ToLower() != "leeftijd")) return voorstellingen;
        if(order == "prijs" || order == "leeftijd") return voorstellingen = await OrderVoorstellingen(voorstellingen, order);
        return StatusCode(500);
    }

    [HttpGet("getvoorstellingen/titel/{titel}")] //DONE
    public async Task<ActionResult<List<Voorstelling>>> GetVoorstellingenByTitel(string titel, [FromQuery] string? order){
        List<Voorstelling> voorstellingen = await _context.Voorstellingen.Where(v => (v.VoorstellingTitel.ToLower()).Contains(titel.ToLower())).ToListAsync();
        if(order == null || (order.ToLower() != "prijs" && order.ToLower() != "leeftijd")) return voorstellingen;
        if(order.ToLower() == "prijs" || order.ToLower() == "leeftijd") return await OrderVoorstellingen(voorstellingen, order);
        return StatusCode(500);    
    }

    [HttpGet("getvoorstellingen")] //DONE
    public async Task<List<Voorstelling>> GetVoorstellingen([FromQuery] string order){
        List<Voorstelling> voorstellingen = await _context.Voorstellingen.ToListAsync();
        if(order == null || (order.ToLower() != "prijs" && order.ToLower() != "leeftijd")) return voorstellingen;
        if(voorstellingen.Count() == 0) return voorstellingen;
        voorstellingen = await OrderVoorstellingen(voorstellingen, order);
        return voorstellingen;
    }


    [HttpPost("AddVoorstelling")] //DONE
    public async Task<ActionResult> AddVoorstelling([FromBody] NieuweVoorstelling nieuweVoorstelling)
    {
        AccessTokenObject accessToken = new AccessTokenObject(){AccessToken = nieuweVoorstelling.AccessToken};
        if(!await _permissionService.IsAllowed(accessToken, "Admin", true, _context) && !await _permissionService.IsAllowed(accessToken, "Medewerker", true, _context)) return StatusCode(403, "No permissions!");
        _kalender = _context.Kalenders.Find(0);
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

    [HttpPost("VerwijderVoorstelling/{id}")] //DONE
    public async Task<ActionResult> VerwijderVoorstelling(int id, [FromBody] AccessTokenObject accessToken)
    {
        if(!await _permissionService.IsAllowed(accessToken, "Admin", true, _context) && !await _permissionService.IsAllowed(accessToken, "Medewerker", true, _context)) return StatusCode(403, "No permissions!");
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

    public async Task<List<Voorstelling>> OrderVoorstellingen(List<Voorstelling> voorstellingen, string order){
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
}

