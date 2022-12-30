using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VoorstellingController : ControllerBase
{
    //nog ff maken

    private readonly GebruikerContext _context;
    private List<Voorstelling> _voorstellingen = new List<Voorstelling>();
    private Kalender _kalender;

    public VoorstellingController(GebruikerContext context)
    {
        _context = context;
    }


    [HttpGet("GetVoorstellingen")]
    public List<Voorstelling> GetVoorstellingen()
    {
        //_kalender = new Kalender(_voorstellingen);
        var response = _voorstellingen;
        return response;
    }

    // [HttpPost("AddVoorstelling")]
    // public async void AddVoorstelling([FromBody] string interval, int aantalKeer, Voorstelling voor)
    // {
    //     _kalender.HerhaalOptie(interval, aantalKeer, voor);
    //     //interval = once, weekly, monthly, yearly
    //     //aantalKeer = hoe vaak
    // }

    [HttpPost("VerwijderVoorstelling")]
    public async void VerwijderVoorstelling([FromBody] Voorstelling voorstelling)
    {
        _voorstellingen.Remove(voorstelling);
    }

}
