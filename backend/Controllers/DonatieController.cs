using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("DonatieListener")]
public class DonatieController : ControllerBase
{

    private readonly GebruikerContext _context;
    public DonatieController(GebruikerContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> NieuweDonatie(){
        //Get JWT token from header
        //Klant klant = await _context.Klanten.FirstOrDefaultAsync(k => k.JWTToken == JWT);
        //Donaties tabel klant toevoegen met hoeveelheid en datum
        //Notificatie maken van een donatie dmv email of iets anders
        //Controleer of gebruiker al donateur is
        //Als dat niet zo is, tellen we alle donaties van deze gebruiker bij elkaar op, als dit hoger dan 1000 is in dit jaar, krijgt de klant de donateur rol
        return Ok();
    }

    [HttpGet("bestelling/{id}")]
    public async Task<ActionResult<Bestelling>> GetBestelling(int id){
        Bestelling bestelling = await _context.Bestellingen.FirstOrDefaultAsync(b => b.BestellingId == id);
        if(bestelling == null) return NotFound();
        return bestelling;
    }
}
