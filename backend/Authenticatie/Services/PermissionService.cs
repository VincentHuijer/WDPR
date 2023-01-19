namespace backend.Authenticatie;
using backend.Controllers;
using Microsoft.EntityFrameworkCore;
public class PermissionService : IPermissionService{
    public async Task<bool> IsAllowed(AccessTokenObject AccessToken, string BenodigdeRol, bool isMedewerker, GebruikerContext _context){
        if(isMedewerker){
            Medewerker medewerker = await GetMedewerkerByAccessToken(AccessToken.AccessToken, _context);
            if(medewerker == null) return false;
            if(medewerker.RolNaam == BenodigdeRol) return true;
            return false;
        }else{
            Klant klant = await GetKlantByAccessToken(AccessToken.AccessToken, _context);
            if(klant == null) return false;
            if(klant.RolNaam == BenodigdeRol) return true;
            return false;
        }
    }

    public async Task<Medewerker> GetMedewerkerByAccessToken(string AccessToken, GebruikerContext _context)
    {
        AccessToken accessToken = await _context.AccessTokens.FirstOrDefaultAsync(a => a.Token == AccessToken);
        if (accessToken == null) return null;
        Medewerker m = await _context.Medewerkers.FirstOrDefaultAsync(m => m.AccessToken == accessToken);
        if (m == null) return null; // error message weghalen, is voor debugging.
        else if (accessToken.VerloopDatum < DateTime.Now) return null;
        return m;
    }

    public async Task<Klant> GetKlantByAccessToken(string AccessToken, GebruikerContext _context){
        AccessToken accessToken = await _context.AccessTokens.FirstOrDefaultAsync(a => a.Token == AccessToken);
        if(accessToken == null) return null;
        Klant k = await _context.Klanten.FirstOrDefaultAsync(k => k.AccessToken == accessToken);
        if(k == null) return null; 
        else if(accessToken.VerloopDatum < DateTime.Now) return null;
        return k;
    }

    public async Task<Klant> GetKlantByEmailAsync(string email, GebruikerContext _context){
        Klant k = await _context.Klanten.FirstOrDefaultAsync(k => k.Email == email);
        return k;
    }

    public async Task<AccessToken> GetAccessTokenByTokenIdAsync(string AccessTokenId, GebruikerContext _context){
        AccessToken accessToken = await _context.AccessTokens.FirstOrDefaultAsync(a => a.Token == AccessTokenId);
        return accessToken;
    }

    public async Task<Medewerker> GetMedewerkerByEmailAsync(string email, GebruikerContext _context)
    {
        Medewerker m = await _context.Medewerkers.FirstOrDefaultAsync(m => m.Email == email);
        return m;
    }
}