﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using backend.Authenticatie;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(GebruikerContext))]
    [Migration("20230106214351_Auth10-Rooster6")]
    partial class Auth10Rooster6
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ActeurVoorstelling", b =>
                {
                    b.Property<int>("ActeurId")
                        .HasColumnType("integer");

                    b.Property<string>("voorstellingTitel")
                        .HasColumnType("text");

                    b.HasKey("ActeurId", "voorstellingTitel");

                    b.HasIndex("voorstellingTitel");

                    b.ToTable("ActeurVoorstellingen");
                });

            modelBuilder.Entity("Kaartjeshouders", b =>
                {
                    b.Property<int>("KlantId")
                        .HasColumnType("integer");

                    b.Property<string>("VoorstellingTitel")
                        .HasColumnType("text");

                    b.HasKey("KlantId", "VoorstellingTitel");

                    b.HasIndex("VoorstellingTitel");

                    b.ToTable("Kaartjeshouders");
                });

            modelBuilder.Entity("Kalender", b =>
                {
                    b.Property<int>("KalenderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("KalenderId"));

                    b.HasKey("KalenderId");

                    b.ToTable("Kalenders");
                });

            modelBuilder.Entity("Stoel", b =>
                {
                    b.Property<int>("StoelID")
                        .HasColumnType("integer");

                    b.Property<bool>("IsGereserveerd")
                        .HasColumnType("boolean");

                    b.Property<double>("Prijs")
                        .HasColumnType("double precision");

                    b.Property<int>("Rang")
                        .HasColumnType("integer");

                    b.HasKey("StoelID");

                    b.ToTable("Stoelen");
                });

            modelBuilder.Entity("Voorstelling", b =>
                {
                    b.Property<string>("VoorstellingTitel")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("KalenderId")
                        .HasColumnType("integer");

                    b.Property<string>("Omschrijving")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Prijs")
                        .HasColumnType("double precision");

                    b.Property<int?>("Zaalnummer")
                        .HasColumnType("integer");

                    b.Property<int>("leeftijd")
                        .HasColumnType("integer");

                    b.HasKey("VoorstellingTitel");

                    b.HasIndex("KalenderId");

                    b.HasIndex("Zaalnummer");

                    b.ToTable("Voorstellingen");
                });

            modelBuilder.Entity("Zaal", b =>
                {
                    b.Property<int>("Zaalnummer")
                        .HasColumnType("integer");

                    b.Property<int>("BeschikbareRangen")
                        .HasColumnType("integer");

                    b.HasKey("Zaalnummer");

                    b.ToTable("Zalen");
                });

            modelBuilder.Entity("backend.Authenticatie.AccessToken", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<DateTime>("VerloopDatum")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Token");

                    b.ToTable("AccessTokens");
                });

            modelBuilder.Entity("backend.Authenticatie.Klant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessTokenId")
                        .HasColumnType("text");

                    b.Property<string>("Achternaam")
                        .HasColumnType("text");

                    b.Property<string>("Afbeelding")
                        .HasColumnType("text");

                    b.Property<bool>("Artiest")
                        .HasColumnType("boolean");

                    b.Property<string>("Beschrijving")
                        .HasColumnType("text");

                    b.Property<bool>("Donateur")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("GeboorteDatum")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Inlogpoging")
                        .HasColumnType("integer");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<string>("RolNaam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TokenId")
                        .HasColumnType("text");

                    b.Property<string>("TwoFactorAuthSecretKey")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorAuthSetupComplete")
                        .HasColumnType("boolean");

                    b.Property<string>("Voornaam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Wachtwoord")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccessTokenId")
                        .IsUnique();

                    b.HasIndex("RolNaam");

                    b.HasIndex("TokenId")
                        .IsUnique();

                    b.ToTable("Klanten");
                });

            modelBuilder.Entity("backend.Authenticatie.Medewerker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessTokenId")
                        .HasColumnType("text");

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Afbeelding")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Functie")
                        .HasColumnType("text");

                    b.Property<DateTime>("GeboorteDatum")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Inlogpoging")
                        .HasColumnType("integer");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<string>("RolNaam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TwoFactorAuthSecretKey")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorAuthSetupComplete")
                        .HasColumnType("boolean");

                    b.Property<string>("Voornaam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Wachtwoord")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccessTokenId")
                        .IsUnique();

                    b.HasIndex("RolNaam");

                    b.ToTable("Medewerkers");
                });

            modelBuilder.Entity("backend.Authenticatie.Rol", b =>
                {
                    b.Property<string>("Naam")
                        .HasColumnType("text");

                    b.HasKey("Naam");

                    b.ToTable("Rollen");
                });

            modelBuilder.Entity("backend.Authenticatie.VerificatieToken", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<DateTime>("VerloopDatum")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Token");

                    b.ToTable("VerificatieTokens");
                });

            modelBuilder.Entity("ActeurVoorstelling", b =>
                {
                    b.HasOne("backend.Authenticatie.Klant", "Acteur")
                        .WithMany("ActeurVoorstelling")
                        .HasForeignKey("ActeurId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Voorstelling", "Voorstelling")
                        .WithMany("Acteur")
                        .HasForeignKey("voorstellingTitel")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Acteur");

                    b.Navigation("Voorstelling");
                });

            modelBuilder.Entity("Kaartjeshouders", b =>
                {
                    b.HasOne("backend.Authenticatie.Klant", "Klant")
                        .WithMany("Kaartjeshouder")
                        .HasForeignKey("KlantId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Voorstelling", "voorstelling")
                        .WithMany("Kaartjeshouder")
                        .HasForeignKey("VoorstellingTitel")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Klant");

                    b.Navigation("voorstelling");
                });

            modelBuilder.Entity("Stoel", b =>
                {
                    b.HasOne("Zaal", "Zaal")
                        .WithMany("Stoelen")
                        .HasForeignKey("StoelID")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Zaal");
                });

            modelBuilder.Entity("Voorstelling", b =>
                {
                    b.HasOne("Kalender", "Kalender")
                        .WithMany("Voorstellingen")
                        .HasForeignKey("KalenderId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Zaal", "Zaal")
                        .WithMany("Voorstellingen")
                        .HasForeignKey("Zaalnummer")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Kalender");

                    b.Navigation("Zaal");
                });

            modelBuilder.Entity("backend.Authenticatie.Klant", b =>
                {
                    b.HasOne("backend.Authenticatie.AccessToken", "AccessToken")
                        .WithOne("Klant")
                        .HasForeignKey("backend.Authenticatie.Klant", "AccessTokenId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("backend.Authenticatie.Rol", "Rol")
                        .WithMany("Klanten")
                        .HasForeignKey("RolNaam")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("backend.Authenticatie.VerificatieToken", "VerificatieToken")
                        .WithOne("Klant")
                        .HasForeignKey("backend.Authenticatie.Klant", "TokenId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("AccessToken");

                    b.Navigation("Rol");

                    b.Navigation("VerificatieToken");
                });

            modelBuilder.Entity("backend.Authenticatie.Medewerker", b =>
                {
                    b.HasOne("backend.Authenticatie.AccessToken", "AccessToken")
                        .WithOne("Medewerker")
                        .HasForeignKey("backend.Authenticatie.Medewerker", "AccessTokenId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("backend.Authenticatie.Rol", "Rol")
                        .WithMany("Medewerkers")
                        .HasForeignKey("RolNaam")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("AccessToken");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("Kalender", b =>
                {
                    b.Navigation("Voorstellingen");
                });

            modelBuilder.Entity("Voorstelling", b =>
                {
                    b.Navigation("Acteur");

                    b.Navigation("Kaartjeshouder");
                });

            modelBuilder.Entity("Zaal", b =>
                {
                    b.Navigation("Stoelen");

                    b.Navigation("Voorstellingen");
                });

            modelBuilder.Entity("backend.Authenticatie.AccessToken", b =>
                {
                    b.Navigation("Klant")
                        .IsRequired();

                    b.Navigation("Medewerker")
                        .IsRequired();
                });

            modelBuilder.Entity("backend.Authenticatie.Klant", b =>
                {
                    b.Navigation("ActeurVoorstelling");

                    b.Navigation("Kaartjeshouder");
                });

            modelBuilder.Entity("backend.Authenticatie.Rol", b =>
                {
                    b.Navigation("Klanten");

                    b.Navigation("Medewerkers");
                });

            modelBuilder.Entity("backend.Authenticatie.VerificatieToken", b =>
                {
                    b.Navigation("Klant")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
