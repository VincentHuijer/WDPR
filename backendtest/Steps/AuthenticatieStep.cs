using System.Linq;
using TechTalk.SpecFlow;
namespace backendtest.Steps;
[Binding]
public sealed class AuthenticatieStep
{
    private IGebruikerService _service;
    private GebruikerContext _context;
    private NieuweKlant _nieuweKlant;
    private EmailWachtwoord _emailWachtwoord;
    private VerificatieToken _token;
    private string _response;
    public AuthenticatieStep(){
        var options = new DbContextOptionsBuilder<GebruikerContext>()
                           .UseInMemoryDatabase("MyInMemoryDb").Options;

        var context = new GebruikerContext(options);
        _context = context;
        var emailServiceMock = new Mock<IEmailService>();
        emailServiceMock.Setup(e => e.Send(It.IsAny<string>(), It.IsAny<string>()));
        var service = new GebruikerService(emailServiceMock.Object);
        _service = service;
    }

    [Given("Een gebruiker met emailadres (.*) heeft nog geen account")]
    public async Task GebruikerGeenAccount(string emailadres){
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
        NieuweKlant nieuweKlant = new NieuweKlant(){Email = emailadres, Wachtwoord = "test", Voornaam = "test", Achternaam = "test"};
        _nieuweKlant = nieuweKlant;
        VerificatieToken token = new VerificatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(3)};
        _token = token;
        await _service.Registreer(_nieuweKlant.Voornaam, _nieuweKlant.Achternaam, _nieuweKlant.Email, _nieuweKlant.Wachtwoord, _token, _context);
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