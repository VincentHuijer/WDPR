using System.Linq;
using TechTalk.SpecFlow;
namespace backendtest.Steps;
[Binding]
public sealed class RegistreerStep
{
    private IGebruikerService _service;
    private GebruikerContext _context;
    private NieuweKlant _nieuweKlant;
    private VerificatieToken _token;
    private string _response;
    public RegistreerStep(){
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
    [Then("moet Success teruggeven worden als response")]
    public async Task ResponseIsSucces(){
        Assert.Equal("Success", _response);
    }


}

public class NieuweKlant{
    public string Email {set; get;}
    public string Wachtwoord {set; get;}
    public string Voornaam {set; get;}
    public string Achternaam {set; get;}
}