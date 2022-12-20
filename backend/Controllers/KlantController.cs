using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class KlantController : ControllerBase
{
    //<<BELANGRIJK>>
    //WILLEN WE GEBRUIK MAKEN VAN DOTNET IDENTITY? IS DIT WEL NODIG? 
    private readonly GebruikerContext _context;
    private IGebruikerService _service = new GebruikerService(); //Is er een andere manier om dit te doen?

    public KlantController(GebruikerContext context)
    {
        _context = context;
    }

    [HttpPost("registreer")]
    public async Task<ActionResult> NieuweKlant([FromBody] Klant klant)
    {
        if(await _service.Registreer(klant.Voornaam, klant.Achternaam, klant.Email, klant.Wachtwoord, _context)) return Ok();
        return BadRequest();
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginKlant([FromBody] Klant klant) //Dit nog veranderen in email/wachtwoord ipv klant
    {
        if(await _service.Login(klant.Email, klant.Wachtwoord, _context, false)) return Ok(); //Bij foute inlogpoging, poging toevoegen aan klant. Bij 3 pogingen, account geblokkeerd. Reset count bij succesvolle login.
        return BadRequest();
    }

    [HttpPost("verifieer")]
    public async Task<ActionResult> VerifieerKlant([FromBody] Klant klant) // Dit nog veranderen in email/token ipv klant
    {
        if(klant.VerificatieToken == null) return BadRequest(); //Klant is al geverifieerd, betere error message hiervoor vinden.
        if(await _service.Verifieer(klant.Email, klant.VerificatieToken.Token, _context)) return Ok();
        return BadRequest();
    }


}