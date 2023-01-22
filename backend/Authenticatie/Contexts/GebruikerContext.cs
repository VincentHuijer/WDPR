using Microsoft.EntityFrameworkCore;
namespace backend.Authenticatie;
public class GebruikerContext : DbContext
{

    public GebruikerContext(DbContextOptions<GebruikerContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Klant>()
            .HasOne(k => k.Rol)
            .WithMany(r => r.Klanten)
            .HasForeignKey(k => k.RolNaam)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<VerificatieToken>()
            .HasOne(vt => vt.Klant)
            .WithOne(k => k.VerificatieToken)
            .HasForeignKey<Klant>(k => k.TokenId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<AccessToken>()
            .HasOne(a => a.Klant)
            .WithOne(k => k.AccessToken)
            .HasForeignKey<Klant>(k => k.AccessTokenId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<AccessToken>()
            .HasOne(a => a.Medewerker)
            .WithOne(m => m.AccessToken)
            .HasForeignKey<Medewerker>(m => m.AccessTokenId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<AuthenticatieToken>()
            .HasOne(a => a.Klant)
            .WithOne(k => k.AuthenticatieToken)
            .HasForeignKey<Klant>(k => k.AuthenticatieTokenId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<AuthenticatieToken>()
            .HasOne(a => a.Medewerker)
            .WithOne(m => m.AuthenticatieToken)
            .HasForeignKey<Medewerker>(m => m.AuthenticatieTokenId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Medewerker>()
            .HasOne(m => m.Rol)
            .WithMany(r => r.Medewerkers)
            .HasForeignKey(m => m.RolNaam)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Bestelling>()
            .HasOne(b => b.Klant)
            .WithMany(k => k.Bestellingen)
            .HasForeignKey(b => b.KlantId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Show>()
            .HasOne(v => v.Kalender)
            .WithMany(k => k.Shows)
            .HasForeignKey(v => v.KalenderId)
            .OnDelete(DeleteBehavior.SetNull);

        //zaal - voorstelling
        modelBuilder.Entity<Show>()
            .HasOne(s => s.Zaal)
            .WithMany(z => z.Shows)
            .HasForeignKey(s => s.Zaalnummer)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Stoel>()
            .HasOne(s => s.Zaal)
            .WithMany(z => z.Stoelen)
            .HasForeignKey(s => s.Zaalnummer)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<BesteldeStoel>()
            .HasKey(b => new { b.StoelID, b.BestellingId, b.Datum });

        modelBuilder.Entity<BesteldeStoel>()
            .HasOne(b => b.Stoel)
            .WithMany(s => s.BesteldeStoelen)
            .HasForeignKey(b => b.StoelID)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<BesteldeStoel>()
            .HasOne(b => b.Bestelling)
            .WithMany(b => b.BesteldeStoelen)
            .HasForeignKey(b => b.BestellingId)
            .OnDelete(DeleteBehavior.SetNull);


        //Groepen
        modelBuilder.Entity<Klant>()
            .HasOne(k => k.ArtiestGroep)
            .WithMany(a => a.Leden)
            .HasForeignKey(k => k.ArtiestGroepId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Show>()
            .HasOne(v => v.ArtiestGroep)
            .WithMany(a => a.Shows)
            .HasForeignKey(v => v.ArtiestGroepId)
            .OnDelete(DeleteBehavior.SetNull);

        //Donatie
        modelBuilder.Entity<Donatie>()
            .HasOne(d => d.Klant)
            .WithMany(k => k.Donaties)
            .HasForeignKey(d => d.KlantId)
            .OnDelete(DeleteBehavior.SetNull);
    }
    public DbSet<Klant> Klanten { set; get; }
    public DbSet<Medewerker> Medewerkers { set; get; }
    public DbSet<Rol> Rollen { set; get; }
    public DbSet<VerificatieToken> VerificatieTokens { set; get; }
    public DbSet<AccessToken> AccessTokens { set; get; }
    public DbSet<AuthenticatieToken> AuthenticatieTokens { set; get; }


    // roostersysteem
    public DbSet<Kalender> Kalenders { set; get; }
    public DbSet<Voorstelling> Voorstellingen { set; get; }
    public DbSet<Zaal> Zalen { set; get; }
    public DbSet<Stoel> Stoelen { set; get; }
    public DbSet<Show> Shows { get; set; }


    //Bestelling
    public DbSet<Bestelling> Bestellingen { get; set; }
    public DbSet<BesteldeStoel> BesteldeStoelen { get; set; }


    //Groepen
    public DbSet<ArtiestGroep> ArtiestGroepen { set; get; }

    //Donaties
    public DbSet<Donatie> Donaties {set; get;}
}