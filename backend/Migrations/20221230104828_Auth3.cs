using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Auth3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessTokenId",
                table: "Medewerkers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccessTokenId",
                table: "Klanten",
                type: "text",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Medewerkers_AccessTokenId",
                table: "Medewerkers",
                column: "AccessTokenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Klanten_AccessTokenId",
                table: "Klanten",
                column: "AccessTokenId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Klanten_AccessTokens_AccessTokenId",
                table: "Klanten",
                column: "AccessTokenId",
                principalTable: "AccessTokens",
                principalColumn: "Token",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Medewerkers_AccessTokens_AccessTokenId",
                table: "Medewerkers",
                column: "AccessTokenId",
                principalTable: "AccessTokens",
                principalColumn: "Token",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klanten_AccessTokens_AccessTokenId",
                table: "Klanten");

            migrationBuilder.DropForeignKey(
                name: "FK_Medewerkers_AccessTokens_AccessTokenId",
                table: "Medewerkers");

            migrationBuilder.DropTable(
                name: "AccessTokens");

            migrationBuilder.DropIndex(
                name: "IX_Medewerkers_AccessTokenId",
                table: "Medewerkers");

            migrationBuilder.DropIndex(
                name: "IX_Klanten_AccessTokenId",
                table: "Klanten");

            migrationBuilder.DropColumn(
                name: "AccessTokenId",
                table: "Medewerkers");

            migrationBuilder.DropColumn(
                name: "AccessTokenId",
                table: "Klanten");
        }
    }
}
