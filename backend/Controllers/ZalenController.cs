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


    [HttpGet("getZalen")]
    public async Task<List<Zaal>> getZalen()
    {
        List<Zaal> Zalen = await _context.Zalen.ToListAsync();
        return Zalen;
    }

    [HttpGet("GetShowStoelen/{id}")]
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

            StoelData stoelData = new StoelData() { X = Stoel.X, Y = Stoel.Y, IsGereserveerd = isGereserveerd, Prijs = Stoel.Prijs, Rang = (isGereserveerd ? 7 : Stoel.Rang), StoelID = Stoel.StoelID };
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



    [HttpPost("AddZaal")]
    public async Task<ActionResult> AddZaal([FromBody] Zaal zaal)
    {

        _context.Zalen.Add(zaal);
        if (await _context.SaveChangesAsync() > 0)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost("VerwijderZaal")]
    public async Task<ActionResult> VerwijderZaal([FromBody] Zaal zaal)
    {
        _context.Zalen.Remove(zaal);
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

public class StoelData
{
    public int X { get; set; }
    public int Y { get; set; }
    public double Prijs { get; set; }
    public int Rang { get; set; }
    public bool IsGereserveerd { get; set; }
    public int StoelID { get; set; }
}