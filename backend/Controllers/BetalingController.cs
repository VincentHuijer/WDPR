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

    [HttpPost] //DONE
    public async Task<ActionResult> Betaling([FromForm] Betaling betaling)
    {
        if(!betaling.succes) return Redirect("https://theater-laak.netlify.app/winkelmand");
        Bestelling bestelling = await _context.Bestellingen.FirstOrDefaultAsync(b => b.BestellingId == betaling.reference);
        if (bestelling == null) return NotFound();
        bestelling.isBetaald = betaling.succes;
        if(bestelling.isBetaald) bestelling.IsActive = false;
        await _context.SaveChangesAsync();
        return Redirect("https://theater-laak.netlify.app/bedankt");
    }





}

