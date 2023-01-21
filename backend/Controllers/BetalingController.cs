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
        // string clientIp = HttpContext.Connection.RemoteIpAddress.ToString();
        // return clientIp;
        // //Authorisatie toevoegen, alleen ip van betaling mag deze request
        if(!betaling.succes) return Redirect("https://theater-laak.netlify.app/winkelmand");
        Bestelling bestelling = await _context.Bestellingen.FirstOrDefaultAsync(b => b.BestellingId == betaling.reference);
        if (bestelling == null) return NotFound();
        bestelling.isBetaald = betaling.succes;
        if(bestelling.isBetaald) bestelling.IsActive = false;
        await _context.SaveChangesAsync();
        return Redirect("https://theater-laak.netlify.app/bedankt");
    }


    // [HttpGet("bestelling/{id}")]
    // public async Task<ActionResult<Bestelling>> GetBestelling(int id)
    // {
    //     //Authorisatie toevoegen
    //     Bestelling bestelling = await _context.Bestellingen.FirstOrDefaultAsync(b => b.BestellingId == id);
    //     if (bestelling == null) return NotFound();
    //     return bestelling;
    // }



}

