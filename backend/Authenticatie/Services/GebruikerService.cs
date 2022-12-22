using Microsoft.EntityFrameworkCore;
public class GebruikerService : IGebruikerService{
//Eventueel tasks omzetten naar actionresults, zie KlantController.

    private IEmailService _emailService;
    public GebruikerService(IEmailService emailService){
        _emailService = emailService;
    }
    public async Task<bool> Registreer(string voornaam, string achternaam, string email, string wachtwoord, GebruikerContext context){
        Klant klant = new Klant(voornaam, achternaam, email, wachtwoord);
        if(await CheckDomainIsDisposable(email)) return false; //Error toevoegen dat duidelijk wordt dat email in temp domain staat.
        if(context.Klanten.Any(k => k.Email == email)) return false; //Error toevoegen dat duidelijk wordt dat email in gebruik is.
        await context.Klanten.AddAsync(klant);
        await context.SaveChangesAsync();
        await _emailService.Send(email, klant.VerificatieToken.Token); //Hier nog juiste content toevoegen
        return true;
    }
    public async Task<bool> Login(string email, string wachtwoord, GebruikerContext context, bool isMedewerker){
        if(!isMedewerker){
            Klant? klant = await context.Klanten.FirstOrDefaultAsync(k => k.Email == email);
            if(klant == null) return false;
            if(klant.Wachtwoord == wachtwoord && klant.VerificatieToken == null) return true;
        }else if(isMedewerker){
            Medewerker? medewerker = await context.Medewerkers.FirstOrDefaultAsync(m => m.Email == email);
            if(medewerker == null) return false;
            if(medewerker.Wachtwoord == wachtwoord) return true;
        }
        return false;
    }
    public async Task<bool> Verifieer(string email, string token, GebruikerContext context){
        Klant? klant = await context.Klanten.FirstOrDefaultAsync(k => k.Email == email);
        if(klant == null) return false;
        if(klant.VerificatieToken.Token == token && klant.VerificatieToken.VerloopDatum > DateTime.Now) return true;
        return false;
    }

    public async Task<bool> CheckDomainIsDisposable(string email){
        string domain = email.Split('@')[1];
        string path = "backend\\Authenticatie\\disposable-emails.txt";
        string[] lines = await File.ReadAllLinesAsync(path);
        if(lines.Any(l => l==domain)){ //Als een van de lines gelijk is aan het domein van het doorgegeven email adres is het een temporary domain. 
            return true;
        }else{
            return false;
        }
    }
}