using Microsoft.EntityFrameworkCore;
namespace backend.Authenticatie;
public class GebruikerContext : DbContext{

    public GebruikerContext(DbContextOptions<GebruikerContext> options) : base(options){
        //DB nog toevoegen. Kunnen kiezen voor Supabase of SQLserver. Nog overleggen.
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<KlantRol>()
        //     .HasKey(kr => new {kr.KlantId, kr.RolNaam});

        // modelBuilder.Entity<Klant>()
        //     .HasMany(k => k.Rollen)
        //     .WithOne(kr => kr.Klant)
        //     .OnDelete(DeleteBehavior.SetNull);

        // modelBuilder.Entity<KlantRol>()
        //     .HasOne(kr => kr.Klant)
        //     .WithMany(k => k.Rollen)
        //     .HasForeignKey(kr => kr.KlantId)
        //     .OnDelete(DeleteBehavior.SetNull);

        // modelBuilder.Entity<Rol>()
        //     .HasMany(r => r.KlantRollen)
        //     .WithOne(kr => kr.Rol)
        //     .OnDelete(DeleteBehavior.SetNull);

        // modelBuilder.Entity<KlantRol>()
        //     .HasOne(kr => kr.Rol)
        //     .WithMany(r => r.KlantRollen)
        //     .HasForeignKey(kr => kr.RolNaam)
        //     .OnDelete(DeleteBehavior.SetNull);
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

        // modelBuilder.Entity<Klant>()
        //     .HasOne(k => k.VerificatieToken)
        //     .WithOne(vt => vt.Klant)
        //     .OnDelete(DeleteBehavior.Cascade);

        // roostersysteem

        // modelBuilder.Entity<Kalender>()
        //     .HasMany(k => k.Voorstellingen)
        //     .WithOne(v => v.Kalender)
        //     .HasForeignKey(k => k.VoorstellingId)
        //     .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Voorstelling>()
            .HasOne(v => v.Kalender)
            .WithMany(k => k.Voorstellingen)
            .HasForeignKey(v => v.KalenderId)
            .OnDelete(DeleteBehavior.SetNull);

        //zaal - voorstelling
        modelBuilder.Entity<Zaal>()
            .HasMany(z => z.Voorstellingen)
            .WithOne(v => v.Zaal)
            .HasForeignKey(v => v.Zaalnummer)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Zaal>()
            .HasMany(z => z.Stoelen)
            .WithOne(s => s.Zaal)
            .HasForeignKey(z => z.StoelID)
            .OnDelete(DeleteBehavior.SetNull);

        // voorstelling - klant (acteur)
        modelBuilder.Entity<ActeurVoorstelling>()
            .HasKey(av => new { av.ActeurId, av.voorstellingTitel });

        modelBuilder.Entity<ActeurVoorstelling>()
            .HasOne(av => av.Acteur)
            .WithMany(k => k.ActeurVoorstelling)
            .HasForeignKey(av => av.ActeurId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<ActeurVoorstelling>()
            .HasOne(av => av.Voorstelling)
            .WithMany(v => v.Acteur)
            .HasForeignKey(av => av.voorstellingTitel)
            .OnDelete(DeleteBehavior.SetNull);

        // voorstelling - klant (kaartjeshouder)
        modelBuilder.Entity<Kaartjeshouders>()
            .HasKey(kh => new { kh.KlantId, kh.VoorstellingTitel });

        modelBuilder.Entity<Kaartjeshouders>()
            .HasOne(kh => kh.Klant)
            .WithMany(k => k.Kaartjeshouder)
            .HasForeignKey(kh => kh.KlantId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Kaartjeshouders>()
            .HasOne(v => v.voorstelling)
            .WithMany(kh => kh.Kaartjeshouder)
            .HasForeignKey(v => v.VoorstellingTitel)
            .OnDelete(DeleteBehavior.SetNull);
        
    
        modelBuilder.Entity<Voorstelling>().Ignore(v => v.BetrokkenPersonen);
        modelBuilder.Entity<Voorstelling>().Ignore(v => v.Datum);


        //bestelling - stoel
        modelBuilder.Entity<Bestelling>()
        .HasKey(b => new {b.BestellingId, b.Stoelen});

        modelBuilder.Entity<Bestelling>()
        .HasMany(b => b.Stoelen)
        .WithOne(s => s.Bestelling)
        .HasForeignKey(b => b.StoelID)
        .OnDelete(DeleteBehavior.SetNull);

        //stoel - zaal (zaal stoel bestaat al boven)
        // modelBuilder.Entity<Stoel>()
        // .HasKey(s => new {s.StoelID, s.Zaalnummer});

        // modelBuilder.Entity<Stoel>()
        // .HasOne(s => s.Zaal)
        // .WithMany(z => z.Stoelen)
        // .HasForeignKey(s => s.Zaal)
        // .OnDelete(DeleteBehavior.SetNull);






        // //Zaal - voorstelling (veel op veel) (Voorstellingregel)
        // modelBuilder.Entity<VoorstellingRegel>()
        //     .HasKey(vr => new {vr.Zaalnummer, vr.VoorstellingTitel});

        // modelBuilder.Entity<VoorstellingRegel>()
        //     .HasOne(vr => vr.voorstelling)
        //     .WithMany(v => v.VoorstellingRegels)
        //     .HasForeignKey(vr => vr.voorstelling)
        //     .OnDelete(DeleteBehavior.SetNull);

        // modelBuilder.Entity<VoorstellingRegel>()
        //     .HasOne(z => z.Zaal)
        //     .WithMany(vr => vr.VoorstellingRegels)
        //     .HasForeignKey(v => v.VoorstellingTitel)
        //     .OnDelete(DeleteBehavior.SetNull);
        
    }
    public DbSet<Klant> Klanten {set; get;}
    public DbSet<Medewerker> Medewerkers {set; get;}
    public DbSet<Rol> Rollen {set; get;}
    public DbSet<VerificatieToken> VerificatieTokens {set; get;}
    public DbSet<AccessToken> AccessTokens {set; get;}
    public DbSet<AuthenticatieToken> AuthenticatieTokens {set; get;}


    // roostersysteem
    public DbSet<Kalender> Kalenders {set; get;}
    public DbSet<Voorstelling> Voorstellingen {set; get;}
    public DbSet<Zaal> Zalen {set; get;}
    public DbSet<Stoel> Stoelen {set; get;}
    public DbSet<Kaartjeshouders> Kaartjeshouders {set; get;}
    public DbSet<ActeurVoorstelling> ActeurVoorstellingen {set; get;}

    
    //Bestelling

    public DbSet<Stoel> stoelen {get; set;}
    public DbSet<Bestelling> Bestellingen {get; set;}


}   