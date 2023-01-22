using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ZaalController : ControllerBase
{
    private readonly GebruikerContext _context;
    private readonly IPermissionService _permissionService = new PermissionService();
    public ZaalController(GebruikerContext context)
    {
        _context = context;
    }



    [HttpPost("GetShowStoelen/{id}")] //DONE
    public async Task<ActionResult<List<List<StoelData>>>> GetShowStoelen([FromBody] AccessTokenObject accessToken, int id)
    {
        if(!await _permissionService.IsAllowed(accessToken, "Medewerker", true, _context) && !await _permissionService.IsAllowed(accessToken, "Admin", true, _context)) return StatusCode(403, "No permissions!");
        await BestellingCleaner.Clean(_context);
        Show show = await _context.Shows.FirstOrDefaultAsync(s => s.ShowId == id);

        List<Stoel> stoelen = await _context.Stoelen.Where(s => s.Zaalnummer == show.Zaalnummer).ToListAsync(); //LIJST VAN ALLE STOELEN

        List<StoelData> stoelDataList = new List<StoelData>();

        foreach (var Stoel in stoelen)
        {
            var besteldeStoel = _context.BesteldeStoelen.Where(stoel => stoel.StoelID == Stoel.StoelID).FirstOrDefault(otherShow => otherShow.Datum == show.Datum);
            bool isGereserveerd = (besteldeStoel != null);
            KlantInfoShort klantInfo = null;
            if(isGereserveerd){
                Bestelling bestelling = await _context.Bestellingen.FirstOrDefaultAsync(b => b.BestellingId == besteldeStoel.BestellingId);
                Klant klant = await _context.Klanten.FirstOrDefaultAsync(k => k.Id == bestelling.KlantId);
                klantInfo = new KlantInfoShort(){Voornaam = klant.Voornaam, Achternaam = klant.Achternaam, Email = klant.Email};
            }
            StoelData stoelData = new StoelData() { X = Stoel.X, Y = Stoel.Y, IsGereserveerd = isGereserveerd, Prijs = Stoel.Prijs, Rang = (isGereserveerd ? 7 : Stoel.Rang), StoelID = Stoel.StoelID, KlantInfo = klantInfo};
            stoelDataList.Add(stoelData);
        }

        List<List<StoelData>> matrix = new List<List<StoelData>>();

        List<int> Rows = stoelDataList.DistinctBy(stoel => stoel.Y).Select(s => s.Y).ToList();

        foreach (int row in Rows)
        {
            List<StoelData> rowData = stoelDataList.Where(stoel => stoel.Y == row).OrderBy(stoel => stoel.X).ToList();
            matrix.Add(rowData);
        }

        return matrix;
    }

    [HttpGet("GetShowStoelenVoorstelling/{id}")] //DONE
    public async Task<ActionResult<List<List<StoelData>>>> GetShowStoelen(int id)
    {
        await BestellingCleaner.Clean(_context);
        Show show = await _context.Shows.FirstOrDefaultAsync(s => s.ShowId == id);

        List<Stoel> stoelen = await _context.Stoelen.Where(s => s.Zaalnummer == show.Zaalnummer).ToListAsync(); //LIJST VAN ALLE STOELEN

        List<StoelData> stoelDataList = new List<StoelData>();

        foreach (var Stoel in stoelen)
        {
            var besteldeStoel = _context.BesteldeStoelen.Where(stoel => stoel.StoelID == Stoel.StoelID).FirstOrDefault(otherShow => otherShow.Datum == show.Datum);
            bool isGereserveerd = (besteldeStoel != null);
            StoelData stoelData = new StoelData() { X = Stoel.X, Y = Stoel.Y, IsGereserveerd = isGereserveerd, Prijs = Stoel.Prijs, Rang = (isGereserveerd ? 7 : Stoel.Rang), StoelID = Stoel.StoelID};
            stoelDataList.Add(stoelData);
        }

        List<List<StoelData>> matrix = new List<List<StoelData>>();

        List<int> Rows = stoelDataList.DistinctBy(stoel => stoel.Y).OrderBy(s => s.Y).Select(s => s.Y).ToList();

        foreach (int row in Rows)
        {
            List<StoelData> rowData = stoelDataList.Where(stoel => stoel.Y == row).OrderBy(stoel => stoel.X).ToList();
            matrix.Add(rowData);
        }

        return matrix;
    }

}

