using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShowController : ControllerBase
{
    private readonly GebruikerContext _context;
    private readonly IPermissionService _permissionService = new PermissionService();
    private Kalender _kalender = new Kalender();
    public ShowController(GebruikerContext context)
    {
        _context = context;
    }

    [HttpGet("GetShows/{voorstellingID}")] //DONE
    public async Task<ActionResult<VoorstellingData>> GetVoorstellingen(int voorstellingID)
    {
        Voorstelling Voorstelling = await _context.Voorstellingen.FirstOrDefaultAsync(v => v.VoorstellingId == voorstellingID);

        List<Show> shows = await _context.Shows.Where(s => s.VoorstellingId == voorstellingID).ToListAsync();

        if (Voorstelling == null) return NotFound();

        return new VoorstellingData() { voorstelling = Voorstelling, shows = shows };
    }

    [HttpPost("AddShow")] //DONE
    public async Task<ActionResult> AddShow([FromBody] HerhaalbareShow HerhaalShow)
    {
        AccessTokenObject accessToken = new AccessTokenObject(){AccessToken = HerhaalShow.AccessToken};
        if(!await _permissionService.IsAllowed(accessToken, "Medewerker", true, _context) && !await _permissionService.IsAllowed(accessToken, "Admin", true, _context)) return StatusCode(403, "No permission!");
        Show show = new Show(HerhaalShow.Zaalnummer, HerhaalShow.StartDatum, HerhaalShow.VoorstellingId, _kalender.KalenderId);

        _context.Shows.Add(show);
        List<Show> showlist = _kalender.HerhaalOptie(HerhaalShow.Interval, HerhaalShow.AantalKeer, show);
        foreach (Show s in showlist)
        {
            _context.Shows.Add(s);
        }

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

