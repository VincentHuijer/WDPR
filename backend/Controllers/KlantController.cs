using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class KlantController : ControllerBase
{
    //<<BELANGRIJK>>
    //WILLEN WE GEBRUIK MAKEN VAN DOTNET IDENTITY? IS DIT WEL NODIG? 
    //WE KUNNEN EIGEN HASHING MAKEN
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
        return BadRequest("Gebruiker bestaat al of emailadres is al in gebruik.");
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginKlant([FromBody] EmailWachtwoord emailWachtwoord)
    {
        if(await _service.Login(emailWachtwoord.Email, emailWachtwoord.Wachtwoord/*Misschien dit wachtwoord gehashed opsturen?*/, _context, false)) return Ok(); //Bij foute inlogpoging, poging toevoegen aan klant. Bij 3 pogingen, account geblokkeerd. Reset count bij succesvolle login.
        return BadRequest("Gebruiker niet gevonden, wachtwoord is fout of emailadres is nog niet geverifieerd."); //Misschien in de service ipv een boolean een actionresult returnen die we hier passen.
    }

    [HttpPost("verifieer")]
    public async Task<ActionResult> VerifieerKlant([FromBody] Klant klant) 
    // Dit nog veranderen in email/token ipv klant. 
    //Hiervoor moeten we token toevoegen aan emailwachtwoord class en een methode maken die een gebruiker kan opvragen op basis van email. Evt een andere manier om dit op te lossen?
    {
        if(klant.VerificatieToken == null) return BadRequest("Already verified");
        if(await _service.Verifieer(klant.Email, klant.VerificatieToken.Token, _context)) return Ok();
        return BadRequest();
    }


}

public class EmailWachtwoord{
    public string Email {set; get;}
    public string Wachtwoord {set; get;}
}
