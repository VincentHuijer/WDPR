using System.Linq;
using TechTalk.SpecFlow;
namespace backendtest.Steps;
[Binding]
public sealed class AuthenticatieStep : CommonStep
{
    private IGebruikerService _service;
    private NieuweKlant _nieuweKlant;
    private EmailWachtwoord _emailWachtwoord;
    private VerificatieToken _token;
    private Klant _klant;
    private string _response;
    private AuthenticatieToken _authToken;
    public AuthenticatieStep() : base(){
        var emailServiceMock = new Mock<IEmailService>();
        emailServiceMock.Setup(e => e.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        var service = new GebruikerService(emailServiceMock.Object);
        _service = service;
    }

    [BeforeScenario]
    public async Task BeforeScenario(){
        _context.AccessTokens.RemoveRange(_context.AccessTokens);
        _context.VerificatieTokens.RemoveRange(_context.VerificatieTokens);
        _context.AuthenticatieTokens.RemoveRange(_context.AuthenticatieTokens);
        _context.Klanten.RemoveRange(_context.Klanten);
        _context.Medewerkers.RemoveRange(_context.Medewerkers);
        _context.Rollen.RemoveRange(_context.Rollen);
        await _context.SaveChangesAsync();
    }

    [Given("Een gebruiker met emailadres (.*) heeft nog geen account")]
    public async Task GebruikerGeenAccount(string emailadres){
        //_context = ContextCreator.CleanContext(_context);
        Klant klant = await _context.Klanten.FirstOrDefaultAsync(k => k.Email == emailadres);
        Assert.Null(klant);
    }
    [Given("gebruiker met emailadres (.*) vult de registratie correct in")]
    public async Task GebruikerVultRegistratieCorrectIn(string emailadres){
        NieuweKlant nieuweKlant = new NieuweKlant(){Email = emailadres, Wachtwoord = "test", Voornaam = "test", Achternaam = "test"};
        _nieuweKlant = nieuweKlant;
        VerificatieToken token = new VerificatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(3)};
        _token = token;
    }
    [When("gebruiker bevestigt de registratie")]
    public async Task GebruikerBevestigtRegistratie(){
        var response = await _service.Registreer(_nieuweKlant.Voornaam, _nieuweKlant.Achternaam, _nieuweKlant.Email, _nieuweKlant.Wachtwoord, _token, _context);
        _response = response;
    }
    [Then("moet (.*) teruggeven worden als response")]
    public async Task Response(string message){
        Assert.Equal(message, _response);
    }

    [Given("Een gebruiker met emailadres (.*) heeft al een account")]
    public async Task GebruikerHeeftAlAccount(string emailadres){
        //_context = ContextCreator.CleanContext(_context);
        NieuweKlant nieuweKlant = new NieuweKlant(){Email = emailadres, Wachtwoord = "test", Voornaam = "test", Achternaam = "test"};
        _nieuweKlant = nieuweKlant;
        VerificatieToken token = new VerificatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(3)};
        _token = token;
        await _service.Registreer(_nieuweKlant.Voornaam, _nieuweKlant.Achternaam, _nieuweKlant.Email, _nieuweKlant.Wachtwoord, _token, _context);
        Klant klant = await _context.Klanten.FirstAsync(k => k.Email == _nieuweKlant.Email);
        klant.VerificatieToken = token;
        klant.TokenId = token.Token;
        await _context.SaveChangesAsync();
    }

    [Given("gebruiker met emailadres (.*) vult de logingegevens correct in")]
    public async Task GebruikerVultLoginGegevensCorrectIn(string emailadres){
        EmailWachtwoord emailWachtwoord = new EmailWachtwoord(){Email = emailadres, Wachtwoord = "test"};
        _emailWachtwoord = emailWachtwoord;
    }
    [Given("gebruiker met emailadres (.*) vult de logingegevens incorrect in")]
    public async Task GebruikerVultLoginGegevensIncorrectIn(string emailadres){
        EmailWachtwoord emailWachtwoord = new EmailWachtwoord(){Email = emailadres, Wachtwoord = "fout"};
        _emailWachtwoord = emailWachtwoord;
    }

    [Given("gebruiker met emailadres (.*) heeft geverifieerd")]
    public async Task GebruikerHeeftGeverifieerd(string emailadres){
        Klant klant = await _context.Klanten.FirstAsync(k => k.Email == emailadres);
        klant.VerificatieToken = null;
        klant.TokenId = null;
        await _context.SaveChangesAsync();
    }

    [Given("gebruiker met emailadres (.*) heeft niet geverifieerd")]
    public async Task GebruikerHeeftNietGeverifieerd(string emailadres){
        Klant klant = await _context.Klanten.FirstAsync(k => k.Email == emailadres);
        Assert.NotNull(klant.TokenId);
    }

    [Given("gebruiker met emailadres (.*) is niet geblokkeerd")]
    public async Task GebruikerIsNietGeblokkeerd(string emailadres){
        Klant klant = await _context.Klanten.FirstAsync(k => k.Email == emailadres);
        klant.IsBlocked = false;
        await _context.SaveChangesAsync();
    }

    [Given("gebruiker met emailadres (.*) is wel geblokkeerd")]
    public async Task GebruikerIsWelGeblokkeerd(string emailadres){
        Klant klant = await _context.Klanten.FirstAsync(k => k.Email == emailadres);
        klant.IsBlocked = true;
        await _context.SaveChangesAsync();
    }
    [Given("gebruiker met emailadres (.*) bezit de juiste token")]
    public async Task GebruikerBezitJuisteToken(string emailadres){
        Klant klant = await _context.Klanten.FirstAsync(k => k.Email == emailadres);
        Assert.Equal(_token.Token, klant.TokenId);
    }
    [Given("gebruiker met emailadres (.*) heeft een niet verlopen token")]
    public async Task GebruikerHeeftNietVerlopenToken(string emailadres){
        Klant klant = await _context.Klanten.FirstAsync(k => k.Email == emailadres);
        VerificatieToken token = await _context.VerificatieTokens.FirstAsync(t => t.Token == klant.TokenId);
        Assert.True(token.VerloopDatum > DateTime.Now);  
    }
    [Given("gebruiker met emailadres (.*) heeft een verlopen token")]
    public async Task GebruikerHeeftVerlopenToken(string emailadres){
        Klant klant = await _context.Klanten.FirstAsync(k => k.Email == emailadres);
        VerificatieToken token = await _context.VerificatieTokens.FirstAsync(t => t.Token == klant.TokenId);
        token.VerloopDatum = token.VerloopDatum.AddDays(-10);
        await _context.SaveChangesAsync();
    }
    [Given("gebruiker met emailadres (.*) bezit geen token meer")]
    public async Task GebruikerHeeftGeenTokenMeer(string emailadres){
        Klant klant = await _context.Klanten.FirstAsync(k => k.Email == emailadres);
        klant.VerificatieToken = null;
        klant.TokenId = null;
        await _context.SaveChangesAsync();
        Assert.Null(klant.TokenId);
    }
    [Given("gebruiker met emailadres (.*) geeft aan zijn wachtwoord te zijn vergeten")]
    public async Task GebruikerIsWachtwoordVergeten(string emailadres){
        Klant klant = await _context.Klanten.FirstAsync(k => k.Email == emailadres);
        await _service.InitiatePasswordReset(klant, _context);
        Assert.NotNull(klant.AuthenticatieTokenId);
    }
    [Given("gebruiker met emailadres (.*) bezit de juiste authenticatietoken")]
    public async Task GebruikerHeeftJuisteAuthenticatieToken(string emailadres){
        Klant klant = await _context.Klanten.FirstAsync(k => k.Email == emailadres);
        AuthenticatieToken authToken = await _context.AuthenticatieTokens.FirstOrDefaultAsync(a => a.Token == klant.AuthenticatieTokenId);
        Assert.NotNull(authToken);
        _authToken = authToken;
    }
    [Given("gebruiker met emailadres (.*) bezit niet de juiste authenticatietoken")]
    public async Task GebruikerHeeftNietJuisteAuthenticatieToken(string emailadres){
        Klant klant = await _context.Klanten.FirstAsync(k => k.Email == emailadres);
        AuthenticatieToken authToken = new AuthenticatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(1)};
        _authToken = authToken;
        await _context.AuthenticatieTokens.AddAsync(authToken);
        await _context.SaveChangesAsync();
    }
    [Given("gebruiker met emailadres (.*) heeft een niet verlopen authenticatietoken")]
    public async Task GebruikerHeeftNietVerlopenAuthenticatieToken(string emailadres){
        Assert.True(_authToken.VerloopDatum > DateTime.Now);
    }
    [Given("gebruiker met emailadres (.*) heeft een verlopen authenticatietoken")]
    public async Task GebruikerHeeftVerlopenAuthenticatieToken(string emailadres){
        Klant klant = await _context.Klanten.FirstAsync(k => k.Email == emailadres);
        AuthenticatieToken authToken = await _context.AuthenticatieTokens.FirstAsync(a => a.Token == klant.AuthenticatieTokenId);
        authToken.VerloopDatum = authToken.VerloopDatum.AddDays(-10);
        await _context.SaveChangesAsync();
        Assert.True(_authToken.VerloopDatum < DateTime.Now);
    }
    [When("gebruiker met emailadres (.*) de wachtwoordreset bevestigt")]
    public async Task GebruikerBevestigtWachtwoordReset(string emailadres){
        Klant klant = await _context.Klanten.FirstAsync(k => k.Email == emailadres);
        var response = await _service.ResetPassword(klant, _authToken.Token, "NieuwWachtwoord123!", _context);
        _response = response;
    }

    [When("gebruiker met emailadres (.*) bevestigt verificatie")]
    public async Task GebruikerBevestigtVerificatie(string emailadres){
        Klant klant = await _context.Klanten.FirstAsync(k => k.Email == emailadres);
        var response = await _service.Verifieer(_nieuweKlant.Email, klant.TokenId, _context);
        _response = response;
    }

    [When("gebruiker bevestigt login")]
    public async Task GebruikerBevestigtLogin(){
        var response = await _service.Login(_emailWachtwoord.Email, _emailWachtwoord.Wachtwoord, _context);
        _response = response;
    }

}

public class NieuweKlant{
    public string Email {set; get;}
    public string Wachtwoord {set; get;}
    public string Voornaam {set; get;}
    public string Achternaam {set; get;}
}

public class EmailWachtwoord{
    public string Email {set; get;}
    public string Wachtwoord {set; get;}
}