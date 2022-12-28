using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Auth1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Afbeelding = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medewerkers", x => x.Id);
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
                    TokenId = table.Column<string>(type: "text", nullable: false),
                    Inlogpoging = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klanten", x => x.Id);
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
                name: "IX_Rollen_MedewerkerId",
                table: "Rollen",
                column: "MedewerkerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Klanten");

            migrationBuilder.DropTable(
                name: "Rollen");

            migrationBuilder.DropTable(
                name: "VerificatieTokens");

            migrationBuilder.DropTable(
                name: "Medewerkers");
        }
    }
}
