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
    [Migration("20230112124509_Auth9-Rooster11-Groepen1")]
    partial class Auth9Rooster11Groepen1
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

                    b.Property<int>("ShowId")
                        .HasColumnType("integer");

                    b.HasKey("ActeurId", "ShowId");

                    b.HasIndex("ShowId");

                    b.ToTable("ActeurVoorstellingen");
                });

            modelBuilder.Entity("ArtiestGroep", b =>
                {
                    b.Property<int>("GroepsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GroepsId"));

                    b.Property<string>("Groepsnaam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Omschrijving")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("GroepsId");

                    b.ToTable("ArtiestGroepen");
                });

            modelBuilder.Entity("BesteldeStoel", b =>
                {
                    b.Property<int>("StoelID")
                        .HasColumnType("integer");

                    b.Property<int>("BestellingId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("StoelID", "BestellingId");

                    b.HasIndex("BestellingId");

                    b.ToTable("BesteldeStoel");
                });

            modelBuilder.Entity("Bestelling", b =>
                {
                    b.Property<int>("BestellingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BestellingId"));

                    b.Property<DateTime>("BestelDatum")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Totaalbedrag")
                        .HasColumnType("integer");

                    b.Property<DateTime>("bestelDatum")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("isBetaald")
                        .HasColumnType("boolean");

                    b.Property<double>("kortingscode")
                        .HasColumnType("double precision");

                    b.HasKey("BestellingId");

                    b.ToTable("Bestellingen");
                });

            modelBuilder.Entity("Kaartjeshouders", b =>
                {
                    b.Property<int>("KlantId")
                        .HasColumnType("integer");

                    b.Property<int>("ShowId")
                        .HasColumnType("integer");

                    b.HasKey("KlantId", "ShowId");

                    b.HasIndex("ShowId");

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

            modelBuilder.Entity("Show", b =>
                {
                    b.Property<int>("ShowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ShowId"));

                    b.Property<int?>("ArtiestGroepId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("KalenderId")
                        .HasColumnType("integer");

                    b.Property<int>("VoorstellingId")
                        .HasColumnType("integer");

                    b.Property<int>("Zaalnummer")
                        .HasColumnType("integer");

                    b.HasKey("ShowId");

                    b.HasIndex("ArtiestGroepId");

                    b.HasIndex("KalenderId");

                    b.HasIndex("VoorstellingId");

                    b.HasIndex("Zaalnummer");

                    b.ToTable("Show");
                });

            modelBuilder.Entity("Stoel", b =>
                {
                    b.Property<int>("StoelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StoelID"));

                    b.Property<bool>("IsGereserveerd")
                        .HasColumnType("boolean");

                    b.Property<double>("Prijs")
                        .HasColumnType("double precision");

                    b.Property<int>("Rang")
                        .HasColumnType("integer");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.Property<int>("Zaalnummer")
                        .HasColumnType("integer");

                    b.HasKey("StoelID");

                    b.HasIndex("Zaalnummer");

                    b.ToTable("Stoel");
                });

            modelBuilder.Entity("Voorstelling", b =>
                {
                    b.Property<int>("VoorstellingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VoorstellingId"));

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Omschrijving")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VoorstellingTitel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("leeftijd")
                        .HasColumnType("integer");

                    b.HasKey("VoorstellingId");

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

            modelBuilder.Entity("backend.Authenticatie.AuthenticatieToken", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<DateTime>("VerloopDatum")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Token");

                    b.ToTable("AuthenticatieTokens");
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

                    b.Property<int?>("ArtiestGroepId")
                        .HasColumnType("integer");

                    b.Property<string>("AuthenticatieTokenId")
                        .HasColumnType("text");

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

                    b.HasIndex("ArtiestGroepId");

                    b.HasIndex("AuthenticatieTokenId")
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

                    b.Property<string>("AuthenticatieTokenId")
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

                    b.HasIndex("AuthenticatieTokenId")
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

                    b.HasOne("Show", "Show")
                        .WithMany("Acteur")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Acteur");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("BesteldeStoel", b =>
                {
                    b.HasOne("Bestelling", "Bestelling")
                        .WithMany("BesteldeStoelen")
                        .HasForeignKey("BestellingId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Stoel", "Stoel")
                        .WithMany("BesteldeStoelen")
                        .HasForeignKey("StoelID")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Bestelling");

                    b.Navigation("Stoel");
                });

            modelBuilder.Entity("Kaartjeshouders", b =>
                {
                    b.HasOne("backend.Authenticatie.Klant", "Klant")
                        .WithMany("Kaartjeshouder")
                        .HasForeignKey("KlantId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Show", "Show")
                        .WithMany("Kaartjeshouder")
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Klant");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("Show", b =>
                {
                    b.HasOne("ArtiestGroep", "ArtiestGroep")
                        .WithMany("Shows")
                        .HasForeignKey("ArtiestGroepId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Kalender", "Kalender")
                        .WithMany("Shows")
                        .HasForeignKey("KalenderId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Voorstelling", "Voorstelling")
                        .WithMany("Shows")
                        .HasForeignKey("VoorstellingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Zaal", "Zaal")
                        .WithMany("Shows")
                        .HasForeignKey("Zaalnummer")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("ArtiestGroep");

                    b.Navigation("Kalender");

                    b.Navigation("Voorstelling");

                    b.Navigation("Zaal");
                });

            modelBuilder.Entity("Stoel", b =>
                {
                    b.HasOne("Zaal", "Zaal")
                        .WithMany("Stoelen")
                        .HasForeignKey("Zaalnummer")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Zaal");
                });

            modelBuilder.Entity("backend.Authenticatie.Klant", b =>
                {
                    b.HasOne("backend.Authenticatie.AccessToken", "AccessToken")
                        .WithOne("Klant")
                        .HasForeignKey("backend.Authenticatie.Klant", "AccessTokenId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ArtiestGroep", "ArtiestGroep")
                        .WithMany("Leden")
                        .HasForeignKey("ArtiestGroepId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("backend.Authenticatie.AuthenticatieToken", "AuthenticatieToken")
                        .WithOne("Klant")
                        .HasForeignKey("backend.Authenticatie.Klant", "AuthenticatieTokenId")
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

                    b.Navigation("ArtiestGroep");

                    b.Navigation("AuthenticatieToken");

                    b.Navigation("Rol");

                    b.Navigation("VerificatieToken");
                });

            modelBuilder.Entity("backend.Authenticatie.Medewerker", b =>
                {
                    b.HasOne("backend.Authenticatie.AccessToken", "AccessToken")
                        .WithOne("Medewerker")
                        .HasForeignKey("backend.Authenticatie.Medewerker", "AccessTokenId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("backend.Authenticatie.AuthenticatieToken", "AuthenticatieToken")
                        .WithOne("Medewerker")
                        .HasForeignKey("backend.Authenticatie.Medewerker", "AuthenticatieTokenId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("backend.Authenticatie.Rol", "Rol")
                        .WithMany("Medewerkers")
                        .HasForeignKey("RolNaam")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("AccessToken");

                    b.Navigation("AuthenticatieToken");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("ArtiestGroep", b =>
                {
                    b.Navigation("Leden");

                    b.Navigation("Shows");
                });

            modelBuilder.Entity("Bestelling", b =>
                {
                    b.Navigation("BesteldeStoelen");
                });

            modelBuilder.Entity("Kalender", b =>
                {
                    b.Navigation("Shows");
                });

            modelBuilder.Entity("Show", b =>
                {
                    b.Navigation("Acteur");

                    b.Navigation("Kaartjeshouder");
                });

            modelBuilder.Entity("Stoel", b =>
                {
                    b.Navigation("BesteldeStoelen");
                });

            modelBuilder.Entity("Voorstelling", b =>
                {
                    b.Navigation("Shows");
                });

            modelBuilder.Entity("Zaal", b =>
                {
                    b.Navigation("Shows");

                    b.Navigation("Stoelen");
                });

            modelBuilder.Entity("backend.Authenticatie.AccessToken", b =>
                {
                    b.Navigation("Klant")
                        .IsRequired();

                    b.Navigation("Medewerker")
                        .IsRequired();
                });

            modelBuilder.Entity("backend.Authenticatie.AuthenticatieToken", b =>
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
