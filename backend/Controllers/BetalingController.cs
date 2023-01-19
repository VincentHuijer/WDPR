using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]


public class BetalingController : ControllerBase
{
    private readonly GebruikerContext _context;
    private readonly IPermissionService _permissionService = new PermissionService();
    public BetalingController(GebruikerContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> Betaling([FromForm] Betaling betaling)
    {
        //Authorisatie toevoegen, alleen ip van betaling mag deze request
        if(!betaling.succes) return Redirect("http://localhost:3000/winkelmand");
        Bestelling bestelling = await _context.Bestellingen.FirstOrDefaultAsync(b => b.BestellingId == betaling.reference);
        if (bestelling == null) return NotFound();
        bestelling.isBetaald = betaling.succes;
        if(bestelling.isBetaald) bestelling.IsActive = false;
        await _context.SaveChangesAsync();
        return Redirect("http://localhost:3000/bedankt");
    }


    [HttpGet("bestelling/{id}")]
    public async Task<ActionResult<Bestelling>> GetBestelling(int id)
    {
        //Authorisatie toevoegen
        Bestelling bestelling = await _context.Bestellingen.FirstOrDefaultAsync(b => b.BestellingId == id);
        if (bestelling == null) return NotFound();
        return bestelling;
    }

    [HttpPost("bestelling")]
    public async Task<ActionResult<Bestelling>> GetBestellingWithAccessToken([FromBody] AccessTokenObject accessTokenObject){
        Klant klant = await _context.Klanten.FirstOrDefaultAsync(k => k.AccessTokenId == accessTokenObject.AccessToken);
        if(klant == null) return NotFound();
        Bestelling bestelling = await _context.Bestellingen.Where(b => b.IsActive == true).FirstOrDefaultAsync(b => b.KlantId == klant.Id);
        if(bestelling == null) return NotFound();
        return bestelling;
    }

}

