using Microsoft.EntityFrameworkCore;
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

        // modelBuilder.Entity<Klant>()
        //     .HasOne(k => k.VerificatieToken)
        //     .WithOne(vt => vt.Klant)
        //     .OnDelete(DeleteBehavior.Cascade);
        
    }
    public DbSet<Klant> Klanten {set; get;}
    public DbSet<Medewerker> Medewerkers {set; get;}
    public DbSet<Rol> Rollen {set; get;}
    public DbSet<VerificatieToken> VerificatieTokens {set; get;}

}