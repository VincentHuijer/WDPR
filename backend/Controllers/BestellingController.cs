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


    // [HttpGet("getBestelling")]
    // public async Task<List<Bestelling>> GetBestellingen()
    // {
    //     //Authorisatie toevoegen
    //     List<Bestelling> bestellingen = await _context.Bestellingen.ToListAsync();
    //     return bestellingen;
    // }

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
        //begin
        List<ShowStoelen> showStoelenList = await GetShowStoelensAsync(bestelling);
        //eind
        return showStoelenList;
    }

    // [HttpPost("kaartjeshouders/{show_id}")]
    // public async Task<ActionResult<List<Kaartjeshouder>>> GetKaartjesHouders([FromBody] AccessTokenObject accessTokenObject, int show_id){
    //     //Authorisatie
    //     if(!await _permissionService.IsAllowed(accessTokenObject, "Medewerker", true, _context)) return StatusCode(403, "No permissions!");
    //     Show show = await _context.Shows.FirstOrDefaultAsync(s => s.ShowId == show_id);
    //     if(show == null) return NotFound();
    //     List<BesteldeStoel> besteldeStoelen = await _context.BesteldeStoelen.Where(b => b.Datum == show.Datum).ToListAsync();
    //     List<int> bestelling_ids = besteldeStoelen.Select(b => b.BestellingId).Distinct().ToList();
    //     List<Bestelling> bestellingen = await _context.Bestellingen.Where(b => bestelling_ids.Contains(b.BestellingId)).ToListAsync();
    //     List<Klant> klanten = await _context.Klanten.Where(k => bestellingen.Select(b => b.KlantId).Contains(k.Id)).ToListAsync();
    //     List<Kaartjeshouder> kaartjeshouders = new List<Kaartjeshouder>();
    //     foreach (var klant in klanten){
    //         kaartjeshouders.Add(new Kaartjeshouder(){Voornaam = klant.Voornaam, Achternaam = klant.Achternaam, Email = klant.Email, Stoelen = await GetStoelDataByEmailAndShow(show_id, klant.Email)});
    //     }
    //     return kaartjeshouders;
    // }

    // [HttpPost("besteldestoelen/{show_id}/{email}")]
    // public async Task<ActionResult<List<StoelData>>> GetBesteldeStoelen([FromBody] AccessTokenObject accessTokenObject, int show_id, string email){
    //     //Authorisatie
    //     if(!await _permissionService.IsAllowed(accessTokenObject, "Medewerker", true, _context)) return StatusCode(403, "No permissions!");
    //     List<StoelData> stoelData = await GetStoelDataByEmailAndShow(show_id, email);
    //     if(stoelData == null) return NotFound();
    //     return stoelData;
    // }

    // [HttpPost("AddBestelling")]
    // public async Task<ActionResult> AddBestelling([FromBody] BestellingBody bestellingBody)
    // {
    //     DateTime bestelDatum = DateTime.Parse(bestellingBody.BestelDatum);
    //     Bestelling bestelling = new Bestelling() { BestelDatum = bestelDatum, Totaalbedrag = bestellingBody.Totaalbedrag };

    //     foreach (var item in bestellingBody.Stoelen)
    //     {
    //         Stoel stoel = await _context.Stoelen.FirstOrDefaultAsync(v => v.StoelID.ToString() == item);
    //         BesteldeStoel besteldeStoel = new BesteldeStoel() { Bestelling = bestelling, BestellingId = bestelling.BestellingId, Stoel = stoel, StoelID = stoel.StoelID, Datum = bestelDatum };

    //         _context.BesteldeStoelen.Add(besteldeStoel);
    //     }

    //     _context.Bestellingen.Add(bestelling);

    //     await _context.SaveChangesAsync();

    //     return Ok();
    // }

    // [HttpPost("VerwijderBestelling")]
    // public async Task<ActionResult> VerwijderBestelling(Bestelling bestelling)
    // {
    //     _context.Bestellingen.Remove(bestelling);
    //     if (await _context.SaveChangesAsync() > 0)
    //     {
    //         return Ok();
    //     }
    //     else
    //     {
    //         return BadRequest();
    //     }
    // }

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
    // public async Task<List<StoelData>> GetStoelDataByEmailAndShow(int show_id, string email){
    //     Show show = await _context.Shows.FirstOrDefaultAsync(s => s.ShowId == show_id);
    //     if(show == null) return null;
    //     Klant klant = await _context.Klanten.FirstOrDefaultAsync(k => k.Email == email);
    //     if(klant == null) return null;
    //     List<int> besteldeStoelen_ids = await _context.BesteldeStoelen.Where(b => b.Datum == show.Datum).Select(b => b.StoelID).ToListAsync();
    //     List<Stoel> stoelen = await _context.Stoelen.Where(s => besteldeStoelen_ids.Contains(s.StoelID)).ToListAsync();
    //     List<StoelData> stoelData = new List<StoelData>();
    //     foreach(var stoel in stoelen){
    //         stoelData.Add(new StoelData(){X = stoel.X, Y = stoel.Y, Prijs = stoel.Prijs, Rang = stoel.Rang, StoelID = stoel.StoelID, IsGereserveerd = true}); 
    //     }
    //     return stoelData;
    // }
    [HttpPost("verwijderbestelling")]
    public async Task<ActionResult> VerwijderBestelling([FromBody] AccessTokenObject accessToken){
        Klant klant = await _permissionService.GetKlantByAccessToken(accessToken.AccessToken, _context);
        Bestelling bestelling = await _context.Bestellingen.Where(b => b.IsActive == true).FirstOrDefaultAsync(b => b.KlantId == klant.Id);
        if(bestelling == null) return NotFound();
        bestelling.IsActive = false;
        await BestellingCleaner.Clean(_context);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("UpdateBestelling")]
    public async Task<ActionResult> UpdateBestelling([FromBody] Bestelling bestelling)
    {
        _context.Bestellingen.Update(bestelling);
        if (await _context.SaveChangesAsync() > 0)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }
}

