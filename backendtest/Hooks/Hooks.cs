using TechTalk.SpecFlow;

[Binding]
public sealed class Hooks : CommonStep{


    public Hooks() : base(){
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

    [AfterScenario]
    public async Task AfterScenario(){
        if(_context != null) _context.Dispose();
    }
}