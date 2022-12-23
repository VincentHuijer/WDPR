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
        var response = HandleResponse(await _service.Login(emailWachtwoord.Email, emailWachtwoord.Wachtwoord/*Misschien dit wachtwoord gehashed opsturen?*/, _context));
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
        var responses = ResponseList.Responses;
        if(responses.ContainsKey(response)){
            return StatusCode(responses[response].Item1, responses[response].Item2);
        }
        return StatusCode(500);
    }

}

public class EmailWachtwoord{
    public string Email {set; get;}
    public string Wachtwoord {set; get;}
}

public class ResponseList{

    public static Dictionary<string, Tuple<int, string>> Responses = new Dictionary<string, Tuple<int, string>>(){
        {"Success", Tuple.Create(200, "Success!")},
        {"AlreadyVerifiedError", Tuple.Create(403, "User already verified!")},
        {"UserNotFoundError", Tuple.Create(400, "User not found!")},
        {"ExpiredTokenError", Tuple.Create(403, "Token expired!")},
        {"NotVerifiedError", Tuple.Create(403, "User not verified!")},
        {"InvalidCredentialsError", Tuple.Create(401, "Email or password incorrect!")},
        {"DisposableMailError", Tuple.Create(406, "Disposable email used!")},
        {"EmailInUseError", Tuple.Create(409, "Email in use!")}
    };
}
