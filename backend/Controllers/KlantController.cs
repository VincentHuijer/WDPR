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
    private IGebruikerService _service = new GebruikerService(new EmailService()); //Is er een andere manier om dit te doen?

    public KlantController(GebruikerContext context)
    {
        _context = context;
    }

    [HttpPost("registreer")]
    public async Task<ActionResult> NieuweKlant([FromBody] Klant klant)
    {   
        var response = HandleResponse(await _service.Registreer(klant.Voornaam, klant.Achternaam, klant.Email, klant.Wachtwoord, _context));
        return response;
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginKlant([FromBody] EmailWachtwoord emailWachtwoord)
    {
        var response = HandleResponse(await _service.Login(emailWachtwoord.Email, emailWachtwoord.Wachtwoord/*Misschien dit wachtwoord gehashed opsturen?*/, _context, false));
        return response;
    }

    [HttpPost("verifieer")]
    public async Task<ActionResult> VerifieerKlant([FromBody] Klant klant) 
    // Dit nog veranderen in email/token ipv klant. 
    // Hiervoor moeten we token toevoegen aan emailwachtwoord class. Evt een andere manier om dit op te lossen?
    {
        var response = HandleResponse(await _service.Verifieer(klant.Email, klant.VerificatieToken.Token, _context));
        return response;
    }

    public ActionResult HandleResponse(string response){
        if(response == ResponseList.Succes) return Ok();
        else if(response == ResponseList.UserNotFoundError) return NotFound(ResponseList.UserNotFoundError);
        else if(response == ResponseList.AlreadyVerifiedError) return StatusCode(403, ResponseList.AlreadyVerifiedError);
        else if(response == ResponseList.ExpiredTokenError) return StatusCode(403, ResponseList.ExpiredTokenError);
        else if(response == ResponseList.NotVerifiedError) return StatusCode(403, ResponseList.NotVerifiedError);
        else if(response == ResponseList.InvalidCredentialsError) return StatusCode(401, ResponseList.InvalidCredentialsError);
        else if(response == ResponseList.DisposableMailError) return StatusCode(406, ResponseList.DisposableMailError);
        else if(response == ResponseList.EmailInUseError) return StatusCode(409, ResponseList.EmailInUseError);
        return StatusCode(500);
    }

}

public class EmailWachtwoord{
    public string Email {set; get;}
    public string Wachtwoord {set; get;}
}

public class ResponseList{
    public static string Succes = "Success!";
    public static string DisposableMailError = "Disposable email used!";
    public static string EmailInUseError = "Email in use!";
    public static string InvalidCredentialsError = "Email or password wrong!";
    public static string ExpiredTokenError = "Token expired!";
    public static string UserNotFoundError = "User not found!";
    public static string AlreadyVerifiedError = "User already verified!";
    public static string NotVerifiedError = "User not verified!";
}
