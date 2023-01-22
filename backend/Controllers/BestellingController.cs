using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Authenticatie;
namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BestellingController : ControllerBase
{
    private readonly GebruikerContext _context;
    private readonly IPermissionService _permissionService = new PermissionService();
    public BestellingController(GebruikerContext context)
    {
        _context = context;
    }


    [HttpPost("getbestelling/by/at")] //DONE
    public async Task<ActionResult<List<ShowStoelen>>> GetBestellingenByAccessToken([FromBody] AccessTokenObject accessTokenObject){
        Klant klant = await _permissionService.GetKlantByAccessToken(accessTokenObject.AccessToken, _context); 
        if(klant == null) return NotFound();
        List<Bestelling> bestellingen = await _context.Bestellingen.Where(b => b.KlantId == klant.Id).Where(b => b.isBetaald == true).ToListAsync();
        if(bestellingen.Count() == 0) return NotFound();
        List<List<ShowStoelen>> showStoelenList = new List<List<ShowStoelen>>(); //Object wat ik return met de benodigde data voor frontend
        foreach(var bestelling in bestellingen){
            showStoelenList.Add(await GetShowStoelensAsync(bestelling));
        }
        List<ShowStoelen> showStoelen = new List<ShowStoelen>();
        foreach(var showStoel in showStoelenList){
            showStoelen.Add(showStoel[0]);
        }
        return showStoelen;
    }

    [HttpPost("nieuwebestelling")] //DONE
    public async Task<ActionResult<string>> NieuweBestelling([FromBody] BestelInfo bestelInfo){

        //Clean up old inactive and unpaid bestellingen
        await BestellingCleaner.Clean(_context);

        //Create new bestelling/add to existing bestelling
        Klant klant = await _context.Klanten.FirstOrDefaultAsync(k => k.AccessTokenId == bestelInfo.AccessToken);
        if(klant == null) return NotFound();
        Show show = _context.Shows.FirstOrDefault(s => s.ShowId == bestelInfo.ShowId);
        if(show == null) return NotFound();
        List<Stoel> Stoelen = new List<Stoel>();
        int Totaalbedrag = 0;

        foreach(var StoelId in bestelInfo.StoelIds){
            Stoel stoel = _context.Stoelen.FirstOrDefault(s => s.StoelID == StoelId);
            Totaalbedrag += (int) stoel.Prijs;
            Stoelen.Add(stoel);
            if(await _context.BesteldeStoelen.Where(s => s.Datum == show.Datum).AnyAsync(s => s.StoelID == stoel.StoelID)) return StatusCode(403, "Stoel bezet");
        }
        bool anyActive = await _context.Bestellingen.Where(b => b.KlantId == klant.Id).AnyAsync(b => b.IsActive);
        Bestelling bestelling = anyActive ? await _context.Bestellingen.Where(b => b.KlantId == klant.Id).FirstAsync(b => b.IsActive) : new Bestelling(){Totaalbedrag = Totaalbedrag, BestelDatum = DateTime.Now, isBetaald = false, kortingscode = 0, Klant = klant, KlantId = klant.Id, IsActive = true};
        if(anyActive){
            bestelling.Totaalbedrag += Totaalbedrag;
        }else{
            await _context.AddAsync(bestelling);
        }
        await _context.SaveChangesAsync();
        foreach(var Stoel in Stoelen){
            BesteldeStoel besteldeStoel = new BesteldeStoel(){Bestelling = bestelling, BestellingId = bestelling.BestellingId, Stoel = Stoel, StoelID = Stoel.StoelID, Datum = show.Datum};
            await _context.AddAsync(besteldeStoel);
        }
        await _context.SaveChangesAsync();
        return bestelling.BestellingId.ToString();
    }

    [HttpPost("activebestelling")] //DONE
    public async Task<ActionResult<List<ShowStoelen>>> GetCurrentActiveBestelling([FromBody] AccessTokenObject accessTokenObject){
        Klant klant = await _context.Klanten.FirstOrDefaultAsync(k => k.AccessTokenId == accessTokenObject.AccessToken);
        if(klant == null) return NotFound();
        Bestelling bestelling = await _context.Bestellingen.Where(b => b.IsActive).FirstOrDefaultAsync(b => b.KlantId == klant.Id);
        if(bestelling == null) return NotFound();

        List<ShowStoelen> showStoelenList = await GetShowStoelensAsync(bestelling);

        return showStoelenList;
    }

    [HttpPost("bestelling")] //DONE
    public async Task<ActionResult<Bestelling>> GetBestellingWithAccessToken([FromBody] AccessTokenObject accessTokenObject){
        Klant klant = await _context.Klanten.FirstOrDefaultAsync(k => k.AccessTokenId == accessTokenObject.AccessToken);
        if(klant == null) return NotFound();
        Bestelling bestelling = await _context.Bestellingen.Where(b => b.IsActive == true).FirstOrDefaultAsync(b => b.KlantId == klant.Id);
        if(bestelling == null) return NotFound();
        return bestelling;
    }

    public async Task<List<ShowStoelen>> GetShowStoelensAsync(Bestelling bestelling){
        List<BesteldeStoel> besteldeStoelen = await _context.BesteldeStoelen.Where(b => b.BestellingId == bestelling.BestellingId).ToListAsync(); //Alle bestelde stoelen die bij deze bestelling horen.
        List<Show> shows = new List<Show>(); //Alle shows die in de bestelling zitten
        List<Stoel> stoelen = new List<Stoel>(); //Alle stoelen die in de bestelling zitten
        foreach(var besteldeStoel in besteldeStoelen){
            Show show = await _context.Shows.FirstAsync(s => s.Datum == besteldeStoel.Datum);
            shows.Add(show);
            Stoel stoel = await _context.Stoelen.FirstAsync(s => s.StoelID == besteldeStoel.StoelID);
            stoelen.Add(stoel);
        }
        shows = shows.Distinct().ToList(); //Duplicate shows verwijderen
        List<ShowStoelen> showStoelenList = new List<ShowStoelen>(); //Object wat ik return met de benodigde data voor frontend
        foreach(var show in shows){
            Voorstelling voorstelling = await _context.Voorstellingen.FirstAsync(v => v.VoorstellingId == show.VoorstellingId);
            List<Stoel> StoelenToAddToShowStoelen = new List<Stoel>();
            foreach(var besteldeStoel in besteldeStoelen){
                if(besteldeStoel.Datum == show.Datum) StoelenToAddToShowStoelen.Add(await _context.Stoelen.FirstAsync(s => s.StoelID == besteldeStoel.StoelID)); //Alle stoelen die specifiek bij deze show horen
            }
            ShowStoelen showStoelen = new ShowStoelen(){ShowId = show.ShowId, ShowNaam = voorstelling.VoorstellingTitel, Stoelen = StoelenToAddToShowStoelen, Datum = show.Datum, ShowImage = voorstelling.Image}; //Info over show met naam x, en aantal stoelen x, info over de stoelen
            showStoelenList.Add(showStoelen);
        }
        return showStoelenList;
    }

    [HttpPost("verwijderbestelling")] //DONE
    public async Task<ActionResult> VerwijderBestelling([FromBody] AccessTokenObject accessToken){
        Klant klant = await _permissionService.GetKlantByAccessToken(accessToken.AccessToken, _context);
        Bestelling bestelling = await _context.Bestellingen.Where(b => b.IsActive == true).FirstOrDefaultAsync(b => b.KlantId == klant.Id);
        if(bestelling == null) return NotFound();
        bestelling.IsActive = false;
        await BestellingCleaner.Clean(_context);
        await _context.SaveChangesAsync();
        return Ok();
    }

}

