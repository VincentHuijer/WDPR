using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Auth9Rooster8Groepen1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stoelen_Zalen_StoelID",
                table: "Stoelen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stoelen",
                table: "Stoelen");

            migrationBuilder.RenameTable(
                name: "Stoelen",
                newName: "Stoel");

            migrationBuilder.AddColumn<int>(
                name: "StoelID",
                table: "Zalen",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zaalnummer",
                table: "Stoel",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stoel",
                table: "Stoel",
                column: "StoelID");

            migrationBuilder.CreateTable(
                name: "Bestellingen",
                columns: table => new
                {
                    BestellingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Totaalbedrag = table.Column<int>(type: "integer", nullable: false),
                    BestelDatum = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    isBetaald = table.Column<bool>(type: "boolean", nullable: false),
                    bestelDatum = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    kortingscode = table.Column<double>(type: "double precision", nullable: false),
                    StoelID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestellingen", x => x.BestellingId);
                    table.ForeignKey(
                        name: "FK_Bestellingen_Stoel_StoelID",
                        column: x => x.StoelID,
                        principalTable: "Stoel",
                        principalColumn: "StoelID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zalen_StoelID",
                table: "Zalen",
                column: "StoelID");

            migrationBuilder.CreateIndex(
                name: "IX_Bestellingen_StoelID",
                table: "Bestellingen",
                column: "StoelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stoel_Bestellingen_StoelID",
                table: "Stoel",
                column: "StoelID",
                principalTable: "Bestellingen",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Stoel_Zalen_StoelID",
                table: "Stoel",
                column: "StoelID",
                principalTable: "Zalen",
                principalColumn: "Zaalnummer",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Zalen_Stoel_StoelID",
                table: "Zalen",
                column: "StoelID",
                principalTable: "Stoel",
                principalColumn: "StoelID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stoel_Bestellingen_StoelID",
                table: "Stoel");

            migrationBuilder.DropForeignKey(
                name: "FK_Stoel_Zalen_StoelID",
                table: "Stoel");

            migrationBuilder.DropForeignKey(
                name: "FK_Zalen_Stoel_StoelID",
                table: "Zalen");

            migrationBuilder.DropTable(
                name: "Bestellingen");

            migrationBuilder.DropIndex(
                name: "IX_Zalen_StoelID",
                table: "Zalen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stoel",
                table: "Stoel");

            migrationBuilder.DropColumn(
                name: "StoelID",
                table: "Zalen");

            migrationBuilder.DropColumn(
                name: "Zaalnummer",
                table: "Stoel");

            migrationBuilder.RenameTable(
                name: "Stoel",
                newName: "Stoelen");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stoelen",
                table: "Stoelen",
                column: "StoelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stoelen_Zalen_StoelID",
                table: "Stoelen",
                column: "StoelID",
                principalTable: "Zalen",
                principalColumn: "Zaalnummer",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
