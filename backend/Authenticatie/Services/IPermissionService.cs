namespace backend.Authenticatie;
using backend.Controllers;
public interface IPermissionService{
    Task<bool> IsAllowed(AccessTokenObject AccessToken, string BenodigdeRol, bool isMedewerker, GebruikerContext _context);
    Task<Medewerker> GetMedewerkerByAccessToken(string AccessToken, GebruikerContext _context);
    Task<Klant> GetKlantByAccessToken(string AccessToken, GebruikerContext _context);
    Task<Klant> GetKlantByEmailAsync(string email, GebruikerContext _context);
    Task<AccessToken> GetAccessTokenByTokenIdAsync(string AccessTokenId, GebruikerContext _context);
    Task<Medewerker> GetMedewerkerByEmailAsync(string email, GebruikerContext _context);
}