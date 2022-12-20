using Microsoft.EntityFrameworkCore;
public class GebruikerContext : DbContext{

    public GebruikerContext(DbContextOptions<GebruikerContext> options) : base(options){
        
    }

    public DbSet<Klant> Klanten {set; get;}
    public DbSet<Medewerker> Medewerkers {set; get;}

}