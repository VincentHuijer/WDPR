using TechTalk.SpecFlow;

[Binding]
public sealed class Hooks{
    private GebruikerContext _context;

    public Hooks(){
        var options = new DbContextOptionsBuilder<GebruikerContext>()
                            .UseInMemoryDatabase("MyInMemoryDb").Options;

        var context = new GebruikerContext(options);
        _context = context;
    }

    [BeforeScenario]
    public async Task BeforeScenario(){
        _context.Rollen.RemoveRange(_context.Rollen);
        _context.AccessTokens.RemoveRange(_context.AccessTokens);
        _context.VerificatieTokens.RemoveRange(_context.VerificatieTokens);
        _context.AuthenticatieTokens.RemoveRange(_context.AuthenticatieTokens);
        _context.Klanten.RemoveRange(_context.Klanten);
        _context.Medewerkers.RemoveRange(_context.Medewerkers);
        await _context.SaveChangesAsync();
    }

    [AfterScenario]
    public async Task AfterScenario(){
        if(_context != null) _context.Dispose();
    }
}