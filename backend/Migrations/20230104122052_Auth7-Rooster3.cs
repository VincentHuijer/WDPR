using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Auth7Rooster3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessTokens_Klanten_KlandId",
                table: "AccessTokens");

            migrationBuilder.DropIndex(
                name: "IX_AccessTokens_KlandId",
                table: "AccessTokens");

            migrationBuilder.DropColumn(
                name: "KlandId",
                table: "AccessTokens");

            migrationBuilder.AddColumn<string>(
                name: "AccessTokenId",
                table: "Klanten",
                type: "text",
                nullable: false,
                defaultValue: "");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Klanten_AccessTokens_AccessTokenId",
                table: "Klanten");

            migrationBuilder.DropIndex(
                name: "IX_Klanten_AccessTokenId",
                table: "Klanten");

            migrationBuilder.DropColumn(
                name: "AccessTokenId",
                table: "Klanten");

            migrationBuilder.AddColumn<int>(
                name: "KlandId",
                table: "AccessTokens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AccessTokens_KlandId",
                table: "AccessTokens",
                column: "KlandId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTokens_Klanten_KlandId",
                table: "AccessTokens",
                column: "KlandId",
                principalTable: "Klanten",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
