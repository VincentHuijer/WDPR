using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Auth11Rooster6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthenticatieTokenId",
                table: "Medewerkers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthenticatieTokenId",
                table: "Klanten",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuthenticatieTokens",
                columns: table => new
                {
                    Token = table.Column<string>(type: "text", nullable: false),
                    VerloopDatum = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthenticatieTokens", x => x.Token);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medewerkers_AuthenticatieTokenId",
                table: "Medewerkers",
                column: "AuthenticatieTokenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Klanten_AuthenticatieTokenId",
                table: "Klanten",
                column: "AuthenticatieTokenId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Klanten_AuthenticatieTokens_AuthenticatieTokenId",
                table: "Klanten",
                column: "AuthenticatieTokenId",
                principalTable: "AuthenticatieTokens",
                principalColumn: "Token",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Medewerkers_AuthenticatieTokens_AuthenticatieTokenId",
                table: "Medewerkers",
                column: "AuthenticatieTokenId",
                principalTable: "AuthenticatieTokens",
                principalColumn: "Token",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klanten_AuthenticatieTokens_AuthenticatieTokenId",
                table: "Klanten");

            migrationBuilder.DropForeignKey(
                name: "FK_Medewerkers_AuthenticatieTokens_AuthenticatieTokenId",
                table: "Medewerkers");

            migrationBuilder.DropTable(
                name: "AuthenticatieTokens");

            migrationBuilder.DropIndex(
                name: "IX_Medewerkers_AuthenticatieTokenId",
                table: "Medewerkers");

            migrationBuilder.DropIndex(
                name: "IX_Klanten_AuthenticatieTokenId",
                table: "Klanten");

            migrationBuilder.DropColumn(
                name: "AuthenticatieTokenId",
                table: "Medewerkers");

            migrationBuilder.DropColumn(
                name: "AuthenticatieTokenId",
                table: "Klanten");
        }
    }
}
