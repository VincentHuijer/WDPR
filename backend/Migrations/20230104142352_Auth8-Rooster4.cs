using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Auth8Rooster4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klanten_Voorstellingen_VoorstellingTitel",
                table: "Klanten");

            migrationBuilder.DropForeignKey(
                name: "FK_Voorstellingen_Klanten_ActeurId",
                table: "Voorstellingen");

            migrationBuilder.DropIndex(
                name: "IX_Voorstellingen_ActeurId",
                table: "Voorstellingen");

            migrationBuilder.DropIndex(
                name: "IX_Klanten_VoorstellingTitel",
                table: "Klanten");

            migrationBuilder.DropColumn(
                name: "ActeurId",
                table: "Voorstellingen");

            migrationBuilder.DropColumn(
                name: "VoorstellingTitel",
                table: "Klanten");

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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActeurVoorstellingen_Voorstellingen_voorstellingTitel",
                        column: x => x.voorstellingTitel,
                        principalTable: "Voorstellingen",
                        principalColumn: "VoorstellingTitel",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kaartjeshouders_Voorstellingen_VoorstellingTitel",
                        column: x => x.VoorstellingTitel,
                        principalTable: "Voorstellingen",
                        principalColumn: "VoorstellingTitel",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActeurVoorstellingen_voorstellingTitel",
                table: "ActeurVoorstellingen",
                column: "voorstellingTitel");

            migrationBuilder.CreateIndex(
                name: "IX_Kaartjeshouders_VoorstellingTitel",
                table: "Kaartjeshouders",
                column: "VoorstellingTitel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActeurVoorstellingen");

            migrationBuilder.DropTable(
                name: "Kaartjeshouders");

            migrationBuilder.AddColumn<int>(
                name: "ActeurId",
                table: "Voorstellingen",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VoorstellingTitel",
                table: "Klanten",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voorstellingen_ActeurId",
                table: "Voorstellingen",
                column: "ActeurId");

            migrationBuilder.CreateIndex(
                name: "IX_Klanten_VoorstellingTitel",
                table: "Klanten",
                column: "VoorstellingTitel");

            migrationBuilder.AddForeignKey(
                name: "FK_Klanten_Voorstellingen_VoorstellingTitel",
                table: "Klanten",
                column: "VoorstellingTitel",
                principalTable: "Voorstellingen",
                principalColumn: "VoorstellingTitel");

            migrationBuilder.AddForeignKey(
                name: "FK_Voorstellingen_Klanten_ActeurId",
                table: "Voorstellingen",
                column: "ActeurId",
                principalTable: "Klanten",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
