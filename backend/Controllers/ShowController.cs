using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShowController : ControllerBase
{
    private readonly GebruikerContext _context;
    public ShowController(GebruikerContext context)
    {
        _context = context;
    }

    [HttpGet("GetShows/{voorstellingID}")]
    public async Task<ActionResult<VoorstellingData>> GetVoorstellingen(int voorstellingID)
    {
        Voorstelling Voorstelling = await _context.Voorstellingen.FirstOrDefaultAsync(v => v.VoorstellingId == voorstellingID);

        List<Show>  shows = await _context.Shows.Where(s => s.VoorstellingId == voorstellingID).ToListAsync();

        if(Voorstelling == null) return NotFound();

        return new VoorstellingData() { voorstelling = Voorstelling, shows = shows};
    }
}

public class VoorstellingData
{
    public Voorstelling voorstelling { get; set; }
    public List<Show> shows { get; set; }
}