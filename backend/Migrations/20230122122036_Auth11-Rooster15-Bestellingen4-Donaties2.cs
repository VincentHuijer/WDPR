using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Auth11Rooster15Bestellingen4Donaties2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActeurVoorstellingen");

            migrationBuilder.DropColumn(
                name: "Afbeelding",
                table: "Medewerkers");

            migrationBuilder.DropColumn(
                name: "Functie",
                table: "Medewerkers");

            migrationBuilder.DropColumn(
                name: "Afbeelding",
                table: "Klanten");

            migrationBuilder.DropColumn(
                name: "Beschrijving",
                table: "Klanten");

            migrationBuilder.DropColumn(
                name: "GeboorteDatum",
                table: "Klanten");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Afbeelding",
                table: "Medewerkers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Functie",
                table: "Medewerkers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Afbeelding",
                table: "Klanten",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Beschrijving",
                table: "Klanten",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GeboorteDatum",
                table: "Klanten",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ActeurVoorstellingen",
                columns: table => new
                {
                    ActeurId = table.Column<int>(type: "integer", nullable: false),
                    ShowId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActeurVoorstellingen", x => new { x.ActeurId, x.ShowId });
                    table.ForeignKey(
                        name: "FK_ActeurVoorstellingen_Klanten_ActeurId",
                        column: x => x.ActeurId,
                        principalTable: "Klanten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ActeurVoorstellingen_Shows_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Shows",
                        principalColumn: "ShowId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActeurVoorstellingen_ShowId",
                table: "ActeurVoorstellingen",
                column: "ShowId");
        }
    }
}
