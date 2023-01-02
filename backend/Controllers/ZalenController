using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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