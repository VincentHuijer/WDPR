using Microsoft.EntityFrameworkCore;
public class GebruikerContext : DbContext{

    public GebruikerContext(DbContextOptions<GebruikerContext> options) : base(options){
        //DB nog toevoegen. Kunnen kiezen voor Supabase of SQLserver. Nog overleggen.
    }

    public DbSet<Klant> Klanten {set; get;}
    public DbSet<Medewerker> Medewerkers {set; get;}

}