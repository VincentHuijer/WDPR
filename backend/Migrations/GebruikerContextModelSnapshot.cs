﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using backend.Authenticatie;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(GebruikerContext))]
    partial class GebruikerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AccessToken", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<DateTime>("VerloopDatum")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Token");

                    b.ToTable("AccessTokens");
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

            modelBuilder.Entity("Klant", b =>
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

                    b.Property<string>("VoorstellingTitel")
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

                    b.HasIndex("VoorstellingTitel");

                    b.ToTable("Klanten");
                });

            modelBuilder.Entity("Medewerker", b =>
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

                    b.Property<string>("ContactGegevens")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Functie")
                        .HasColumnType("text");

                    b.Property<DateTime>("GeboorteDatum")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Voornaam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Wachtwoord")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AccessTokenId")
                        .IsUnique();

                    b.ToTable("Medewerkers");
                });

            modelBuilder.Entity("Rol", b =>
                {
                    b.Property<string>("Naam")
                        .HasColumnType("text");

                    b.Property<int?>("MedewerkerId")
                        .HasColumnType("integer");

                    b.HasKey("Naam");

                    b.HasIndex("MedewerkerId");

                    b.ToTable("Rollen");
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

            modelBuilder.Entity("VerificatieToken", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<DateTime>("VerloopDatum")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Token");

                    b.ToTable("VerificatieTokens");
                });

            modelBuilder.Entity("Voorstelling", b =>
                {
                    b.Property<string>("VoorstellingTitel")
                        .HasColumnType("text");

                    b.Property<int>("ActeurId")
                        .HasColumnType("integer");

                    b.Property<List<string>>("BetrokkenPersonen")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<DateTime>("DatumEnTijd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("KalenderId")
                        .HasColumnType("integer");

                    b.Property<string>("Omschrijving")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Prijs")
                        .HasColumnType("double precision");

                    b.Property<int>("Zaalnummer")
                        .HasColumnType("integer");

                    b.HasKey("VoorstellingTitel");

                    b.HasIndex("ActeurId");

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

            modelBuilder.Entity("Klant", b =>
                {
                    b.HasOne("AccessToken", "AccessToken")
                        .WithOne("Klant")
                        .HasForeignKey("Klant", "AccessTokenId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Rol", "Rol")
                        .WithMany("Klanten")
                        .HasForeignKey("RolNaam")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("VerificatieToken", "VerificatieToken")
                        .WithOne("Klant")
                        .HasForeignKey("Klant", "TokenId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Voorstelling", null)
                        .WithMany("Kaartjeshouders")
                        .HasForeignKey("VoorstellingTitel");

                    b.Navigation("AccessToken");

                    b.Navigation("Rol");

                    b.Navigation("VerificatieToken");
                });

            modelBuilder.Entity("Medewerker", b =>
                {
                    b.HasOne("AccessToken", "AccessToken")
                        .WithOne("Medewerker")
                        .HasForeignKey("Medewerker", "AccessTokenId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("AccessToken");
                });

            modelBuilder.Entity("Rol", b =>
                {
                    b.HasOne("Medewerker", null)
                        .WithMany("Rollen")
                        .HasForeignKey("MedewerkerId");
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
                    b.HasOne("Klant", "Acteur")
                        .WithMany()
                        .HasForeignKey("ActeurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kalender", "Kalender")
                        .WithMany("Voorstellingen")
                        .HasForeignKey("KalenderId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Zaal", "Zaal")
                        .WithMany("Voorstellingen")
                        .HasForeignKey("Zaalnummer")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Acteur");

                    b.Navigation("Kalender");

                    b.Navigation("Zaal");
                });

            modelBuilder.Entity("AccessToken", b =>
                {
                    b.Navigation("Klant")
                        .IsRequired();

                    b.Navigation("Medewerker")
                        .IsRequired();
                });

            modelBuilder.Entity("Kalender", b =>
                {
                    b.Navigation("Voorstellingen");
                });

            modelBuilder.Entity("Medewerker", b =>
                {
                    b.Navigation("Rollen");
                });

            modelBuilder.Entity("Rol", b =>
                {
                    b.Navigation("Klanten");
                });

            modelBuilder.Entity("VerificatieToken", b =>
                {
                    b.Navigation("Klant")
                        .IsRequired();
                });

            modelBuilder.Entity("Voorstelling", b =>
                {
                    b.Navigation("Kaartjeshouders");
                });

            modelBuilder.Entity("Zaal", b =>
                {
                    b.Navigation("Stoelen");

                    b.Navigation("Voorstellingen");
                });
#pragma warning restore 612, 618
        }
    }
}
