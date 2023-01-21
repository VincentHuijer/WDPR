using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("DonatieListener")]
public class DonatieListenerController : ControllerBase
{

    private readonly GebruikerContext _context;
    public DonatieListenerController(GebruikerContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> NieuweDonatie([FromBody] DonatieObject donatieObject){
        Klant klant = await _context.Klanten.FirstOrDefaultAsync(k => k.Email == donatieObject.Email);
        if(klant == null) return Ok("Donatie succesvol, maar gebruiker is niet gevonden!");
        if(donatieObject.Hoeveelheid <= 0) return BadRequest();
        Donatie donatie = new Donatie(){Hoeveelheid = donatieObject.Hoeveelheid, Email = donatieObject.Email, Naam = donatieObject.Naam, Datum = DateTime.Now, Klant = klant, KlantId = klant.Id};
        await _context.Donaties.AddAsync(donatie);
        await _context.SaveChangesAsync();
        try{
            await DonateurCheck.DonateurStatusCheck(klant, _context);
            return Ok();
        }catch(Exception e){
            return BadRequest(e);
        }
        //Get JWT token from header
        //Klant klant = await _context.Klanten.FirstOrDefaultAsync(k => k.JWTToken == JWT);
        //Donaties tabel klant toevoegen met hoeveelheid en datum
        //Notificatie maken van een donatie dmv email of iets anders
        //Controleer of gebruiker al donateur is
    }

    // [HttpGet("bestelling/{id}")]
    // public async Task<ActionResult<Bestelling>> GetBestelling(int id){
    //     Bestelling bestelling = await _context.Bestellingen.FirstOrDefaultAsync(b => b.BestellingId == id);
    //     if(bestelling == null) return NotFound();
    //     return bestelling;
    // }
    public async Task<bool> DonateurStatus(Klant klant){
        List<Donatie> DonatiesDitJaar = await _context.Donaties.Where(d => d.Datum > DateTime.Now.AddYears(-1)).ToListAsync();
        double totaal = 0;
        foreach(var Donatie in DonatiesDitJaar){
            totaal += Donatie.Hoeveelheid;
        }
        if(totaal >= 1000) return true;
        return false;
    }
}
