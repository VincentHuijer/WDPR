using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Auth9Rooster6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessTokens",
                columns: table => new
                {
                    Token = table.Column<string>(type: "text", nullable: false),
                    VerloopDatum = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessTokens", x => x.Token);
                });

            migrationBuilder.CreateTable(
                name: "Kalenders",
                columns: table => new
                {
                    KalenderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kalenders", x => x.KalenderId);
                });

            migrationBuilder.CreateTable(
                name: "VerificatieTokens",
                columns: table => new
                {
                    Token = table.Column<string>(type: "text", nullable: false),
                    VerloopDatum = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificatieTokens", x => x.Token);
                });

            migrationBuilder.CreateTable(
                name: "Zalen",
                columns: table => new
                {
                    Zaalnummer = table.Column<int>(type: "integer", nullable: false),
                    BeschikbareRangen = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zalen", x => x.Zaalnummer);
                });

            migrationBuilder.CreateTable(
                name: "Medewerkers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Voornaam = table.Column<string>(type: "text", nullable: false),
                    Achternaam = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Wachtwoord = table.Column<string>(type: "text", nullable: false),
                    Functie = table.Column<string>(type: "text", nullable: true),
                    ContactGegevens = table.Column<string>(type: "text", nullable: true),
                    GeboorteDatum = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Afbeelding = table.Column<string>(type: "text", nullable: true),
                    AccessTokenId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medewerkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medewerkers_AccessTokens_AccessTokenId",
                        column: x => x.AccessTokenId,
                        principalTable: "AccessTokens",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Stoelen",
                columns: table => new
                {
                    StoelID = table.Column<int>(type: "integer", nullable: false),
                    IsGereserveerd = table.Column<bool>(type: "boolean", nullable: false),
                    Rang = table.Column<int>(type: "integer", nullable: false),
                    Prijs = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stoelen", x => x.StoelID);
                    table.ForeignKey(
                        name: "FK_Stoelen_Zalen_StoelID",
                        column: x => x.StoelID,
                        principalTable: "Zalen",
                        principalColumn: "Zaalnummer",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Voorstellingen",
                columns: table => new
                {
                    VoorstellingTitel = table.Column<string>(type: "text", nullable: false),
                    KalenderId = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    leeftijd = table.Column<int>(type: "integer", nullable: false),
                    Omschrijving = table.Column<string>(type: "text", nullable: false),
                    Prijs = table.Column<double>(type: "double precision", nullable: false),
                    Zaalnummer = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voorstellingen", x => x.VoorstellingTitel);
                    table.ForeignKey(
                        name: "FK_Voorstellingen_Kalenders_KalenderId",
                        column: x => x.KalenderId,
                        principalTable: "Kalenders",
                        principalColumn: "KalenderId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Voorstellingen_Zalen_Zaalnummer",
                        column: x => x.Zaalnummer,
                        principalTable: "Zalen",
                        principalColumn: "Zaalnummer",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Rollen",
                columns: table => new
                {
                    Naam = table.Column<string>(type: "text", nullable: false),
                    MedewerkerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rollen", x => x.Naam);
                    table.ForeignKey(
                        name: "FK_Rollen_Medewerkers_MedewerkerId",
                        column: x => x.MedewerkerId,
                        principalTable: "Medewerkers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Klanten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Voornaam = table.Column<string>(type: "text", nullable: false),
                    Achternaam = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Wachtwoord = table.Column<string>(type: "text", nullable: false),
                    Beschrijving = table.Column<string>(type: "text", nullable: true),
                    Afbeelding = table.Column<string>(type: "text", nullable: true),
                    GeboorteDatum = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Donateur = table.Column<bool>(type: "boolean", nullable: false),
                    Artiest = table.Column<bool>(type: "boolean", nullable: false),
                    RolNaam = table.Column<string>(type: "text", nullable: false),
                    TokenId = table.Column<string>(type: "text", nullable: true),
                    Inlogpoging = table.Column<int>(type: "integer", nullable: false),
                    AccessTokenId = table.Column<string>(type: "text", nullable: true),
                    TwoFactorAuthSecretKey = table.Column<string>(type: "text", nullable: true),
                    TwoFactorAuthSetupComplete = table.Column<bool>(type: "boolean", nullable: false),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klanten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Klanten_AccessTokens_AccessTokenId",
                        column: x => x.AccessTokenId,
                        principalTable: "AccessTokens",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Klanten_Rollen_RolNaam",
                        column: x => x.RolNaam,
                        principalTable: "Rollen",
                        principalColumn: "Naam",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Klanten_VerificatieTokens_TokenId",
                        column: x => x.TokenId,
                        principalTable: "VerificatieTokens",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ActeurVoorstellingen",
                columns: table => new
                {
                    voorstellingTitel = table.Column<string>(type: "text", nullable: false),
                    ActeurId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActeurVoorstellingen", x => new { x.ActeurId, x.voorstellingTitel });
                    table.ForeignKey(
                        name: "FK_ActeurVoorstellingen_Klanten_ActeurId",
                        column: x => x.ActeurId,
                        principalTable: "Klanten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ActeurVoorstellingen_Voorstellingen_voorstellingTitel",
                        column: x => x.voorstellingTitel,
                        principalTable: "Voorstellingen",
                        principalColumn: "VoorstellingTitel",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Kaartjeshouders",
                columns: table => new
                {
                    VoorstellingTitel = table.Column<string>(type: "text", nullable: false),
                    KlantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kaartjeshouders", x => new { x.KlantId, x.VoorstellingTitel });
                    table.ForeignKey(
                        name: "FK_Kaartjeshouders_Klanten_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Klanten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Kaartjeshouders_Voorstellingen_VoorstellingTitel",
                        column: x => x.VoorstellingTitel,
                        principalTable: "Voorstellingen",
                        principalColumn: "VoorstellingTitel",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActeurVoorstellingen_voorstellingTitel",
                table: "ActeurVoorstellingen",
                column: "voorstellingTitel");

            migrationBuilder.CreateIndex(
                name: "IX_Kaartjeshouders_VoorstellingTitel",
                table: "Kaartjeshouders",
                column: "VoorstellingTitel");

            migrationBuilder.CreateIndex(
                name: "IX_Klanten_AccessTokenId",
                table: "Klanten",
                column: "AccessTokenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Klanten_RolNaam",
                table: "Klanten",
                column: "RolNaam");

            migrationBuilder.CreateIndex(
                name: "IX_Klanten_TokenId",
                table: "Klanten",
                column: "TokenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medewerkers_AccessTokenId",
                table: "Medewerkers",
                column: "AccessTokenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rollen_MedewerkerId",
                table: "Rollen",
                column: "MedewerkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Voorstellingen_KalenderId",
                table: "Voorstellingen",
                column: "KalenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Voorstellingen_Zaalnummer",
                table: "Voorstellingen",
                column: "Zaalnummer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActeurVoorstellingen");

            migrationBuilder.DropTable(
                name: "Kaartjeshouders");

            migrationBuilder.DropTable(
                name: "Stoelen");

            migrationBuilder.DropTable(
                name: "Klanten");

            migrationBuilder.DropTable(
                name: "Voorstellingen");

            migrationBuilder.DropTable(
                name: "Rollen");

            migrationBuilder.DropTable(
                name: "VerificatieTokens");

            migrationBuilder.DropTable(
                name: "Kalenders");

            migrationBuilder.DropTable(
                name: "Zalen");

            migrationBuilder.DropTable(
                name: "Medewerkers");

            migrationBuilder.DropTable(
                name: "AccessTokens");
        }
    }
}
