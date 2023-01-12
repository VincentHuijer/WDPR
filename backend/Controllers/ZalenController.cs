using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ZaalController : ControllerBase
{
    private readonly GebruikerContext _context;
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

   [HttpGet("GetZaalWithId/{id}")]
    public async Task<ActionResult<Zaal>> GetZaalWithId(int id)
    {
        Zaal z = await _context.Zalen.FirstOrDefaultAsync(v => v.Zaalnummer == id);
        if(z == null){
            return NotFound();
        }

        return z;
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