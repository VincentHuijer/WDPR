using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Auth8Rooster7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActeurVoorstellingen_Voorstellingen_voorstellingTitel",
                table: "ActeurVoorstellingen");

            migrationBuilder.DropForeignKey(
                name: "FK_Kaartjeshouders_Voorstellingen_VoorstellingTitel",
                table: "Kaartjeshouders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Voorstellingen",
                table: "Voorstellingen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kaartjeshouders",
                table: "Kaartjeshouders");

            migrationBuilder.DropIndex(
                name: "IX_Kaartjeshouders_VoorstellingTitel",
                table: "Kaartjeshouders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActeurVoorstellingen",
                table: "ActeurVoorstellingen");

            migrationBuilder.DropIndex(
                name: "IX_ActeurVoorstellingen_voorstellingTitel",
                table: "ActeurVoorstellingen");

            migrationBuilder.DropColumn(
                name: "VoorstellingTitel",
                table: "Kaartjeshouders");

            migrationBuilder.DropColumn(
                name: "voorstellingTitel",
                table: "ActeurVoorstellingen");

            migrationBuilder.AddColumn<int>(
                name: "VoorstellingId",
                table: "Voorstellingen",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "VoorstellingId",
                table: "Kaartjeshouders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VoorstellingId",
                table: "ActeurVoorstellingen",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Voorstellingen",
                table: "Voorstellingen",
                column: "VoorstellingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kaartjeshouders",
                table: "Kaartjeshouders",
                columns: new[] { "KlantId", "VoorstellingId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActeurVoorstellingen",
                table: "ActeurVoorstellingen",
                columns: new[] { "ActeurId", "VoorstellingId" });

            migrationBuilder.CreateIndex(
                name: "IX_Kaartjeshouders_VoorstellingId",
                table: "Kaartjeshouders",
                column: "VoorstellingId");

            migrationBuilder.CreateIndex(
                name: "IX_ActeurVoorstellingen_VoorstellingId",
                table: "ActeurVoorstellingen",
                column: "VoorstellingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActeurVoorstellingen_Voorstellingen_VoorstellingId",
                table: "ActeurVoorstellingen",
                column: "VoorstellingId",
                principalTable: "Voorstellingen",
                principalColumn: "VoorstellingId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Kaartjeshouders_Voorstellingen_VoorstellingId",
                table: "Kaartjeshouders",
                column: "VoorstellingId",
                principalTable: "Voorstellingen",
                principalColumn: "VoorstellingId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActeurVoorstellingen_Voorstellingen_VoorstellingId",
                table: "ActeurVoorstellingen");

            migrationBuilder.DropForeignKey(
                name: "FK_Kaartjeshouders_Voorstellingen_VoorstellingId",
                table: "Kaartjeshouders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Voorstellingen",
                table: "Voorstellingen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kaartjeshouders",
                table: "Kaartjeshouders");

            migrationBuilder.DropIndex(
                name: "IX_Kaartjeshouders_VoorstellingId",
                table: "Kaartjeshouders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActeurVoorstellingen",
                table: "ActeurVoorstellingen");

            migrationBuilder.DropIndex(
                name: "IX_ActeurVoorstellingen_VoorstellingId",
                table: "ActeurVoorstellingen");

            migrationBuilder.DropColumn(
                name: "VoorstellingId",
                table: "Voorstellingen");

            migrationBuilder.DropColumn(
                name: "VoorstellingId",
                table: "Kaartjeshouders");

            migrationBuilder.DropColumn(
                name: "VoorstellingId",
                table: "ActeurVoorstellingen");

            migrationBuilder.AddColumn<string>(
                name: "VoorstellingTitel",
                table: "Kaartjeshouders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "voorstellingTitel",
                table: "ActeurVoorstellingen",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Voorstellingen",
                table: "Voorstellingen",
                column: "VoorstellingTitel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kaartjeshouders",
                table: "Kaartjeshouders",
                columns: new[] { "KlantId", "VoorstellingTitel" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActeurVoorstellingen",
                table: "ActeurVoorstellingen",
                columns: new[] { "ActeurId", "voorstellingTitel" });

            migrationBuilder.CreateIndex(
                name: "IX_Kaartjeshouders_VoorstellingTitel",
                table: "Kaartjeshouders",
                column: "VoorstellingTitel");

            migrationBuilder.CreateIndex(
                name: "IX_ActeurVoorstellingen_voorstellingTitel",
                table: "ActeurVoorstellingen",
                column: "voorstellingTitel");

            migrationBuilder.AddForeignKey(
                name: "FK_ActeurVoorstellingen_Voorstellingen_voorstellingTitel",
                table: "ActeurVoorstellingen",
                column: "voorstellingTitel",
                principalTable: "Voorstellingen",
                principalColumn: "VoorstellingTitel",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Kaartjeshouders_Voorstellingen_VoorstellingTitel",
                table: "Kaartjeshouders",
                column: "VoorstellingTitel",
                principalTable: "Voorstellingen",
                principalColumn: "VoorstellingTitel",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
