using System.Linq;
using backend.Controllers;
using TechTalk.SpecFlow;
namespace backendtest.Steps;
[Binding]
public sealed class AuthorisatieStep
{
    private IPermissionService _permissionService;
    private IGebruikerService _gebruikerService;
    private IMedewerkerService _medewerkerService;
    private AccessToken _accessToken;
    private GebruikerContext _context;
    private Klant? _klant;
    private Medewerker? _medewerker;
    private string _benodigdeRol;
    private bool _result;
    public AuthorisatieStep(){
        var options = new DbContextOptionsBuilder<GebruikerContext>()
                           .UseInMemoryDatabase("MyInMemoryDb").Options;

        var context = new GebruikerContext(options);
        _context = context;
        var permissionService = new PermissionService();
        _permissionService = permissionService;
        var emailServiceMock = new Mock<IEmailService>();
        emailServiceMock.Setup(e => e.Send(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        var gebruikerService = new GebruikerService(emailServiceMock.Object);
        _gebruikerService = gebruikerService;
        var medewerkerService = new MedewerkerService();
        _medewerkerService = medewerkerService;
    }
    [BeforeScenario]
    public async Task BeforeScenario(){
        _context.AccessTokens.RemoveRange(_context.AccessTokens);
        _context.VerificatieTokens.RemoveRange(_context.VerificatieTokens);
        _context.AuthenticatieTokens.RemoveRange(_context.AuthenticatieTokens);
        _context.Klanten.RemoveRange(_context.Klanten);
        _context.Medewerkers.RemoveRange(_context.Medewerkers);
        _context.AuthenticatieTokens.RemoveRange(_context.AuthenticatieTokens);
        _context.Rollen.RemoveRange(_context.Rollen);
        await _context.SaveChangesAsync();
    }
    [Given("een medewerker met rol (.*)")]
    public async Task MedewerkerMetRol(string rol){
        var response = await _medewerkerService.NieuweMedewerker("Test", "Test", "somethingrandom@gmail.com", "Test123!", _context);
        Assert.Equal("Success", response);
        Medewerker medewerker = await _context.Medewerkers.FirstAsync(m => m.Email == "somethingrandom@gmail.com");
        AccessToken accessToken = new AccessToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(1)};
        await _context.AccessTokens.AddAsync(accessToken);
        await _context.SaveChangesAsync();
        Assert.NotNull(medewerker);
        medewerker.RolNaam = rol;
        medewerker.AccessTokenId = accessToken.Token;
        await _context.SaveChangesAsync();
        _medewerker = medewerker;
        _accessToken = accessToken;
    }

    [Given("een klant met rol (.*)")]
    public async Task KlantMetRol(string rol){
        var response = await _gebruikerService.Registreer("Test", "Test", "somethingrandom@gmail.com", "Test123!", new VerificatieToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(3)}, _context);
        Assert.Equal("Success", response);
        Klant klant = await _context.Klanten.FirstAsync(m => m.Email == "somethingrandom@gmail.com");
        AccessToken accessToken = new AccessToken(){Token = Guid.NewGuid().ToString(), VerloopDatum = DateTime.Now.AddDays(1)};
        await _context.AccessTokens.AddAsync(accessToken);
        await _context.SaveChangesAsync();
        Assert.NotNull(klant);
        klant.RolNaam = rol;
        klant.AccessTokenId = accessToken.Token;
        await _context.SaveChangesAsync();
        _klant = klant;
        _accessToken = accessToken;
    }

    [Given("een bepaalde functie vereist rol (.*)")]
    public async Task FunctieVereistRol(string benodigdeRol){
        _benodigdeRol = benodigdeRol;
    }

    [When("medewerker met rol (.*) deze functie probeert uit te voeren")]
    public async Task MedewerkerVoertFunctieUit(string rol){
        AccessTokenObject accessTokenObject = new AccessTokenObject(){AccessToken = _accessToken.Token};
        var result = await _permissionService.IsAllowed(accessTokenObject, _benodigdeRol, true, _context);
        _result = result;
    }

    [When("klant met rol (.*) deze functie probeert uit te voeren")]
    public async Task KlantVoertFunctieUit(string rol){
        AccessTokenObject accessTokenObject = new AccessTokenObject(){AccessToken = _accessToken.Token};
        var result = await _permissionService.IsAllowed(accessTokenObject, _benodigdeRol, true, _context);
        _result = result;
    }

    [Then("moet (.*) teruggeven worden als waarde")]
    public async Task Response(bool value){
        Assert.Equal(value, _result);
    }

}