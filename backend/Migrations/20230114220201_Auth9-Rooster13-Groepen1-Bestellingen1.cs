using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Auth9Rooster13Groepen1Bestellingen1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KlantId",
                table: "Bestellingen",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bestellingen_KlantId",
                table: "Bestellingen",
                column: "KlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bestellingen_Klanten_KlantId",
                table: "Bestellingen",
                column: "KlantId",
                principalTable: "Klanten",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bestellingen_Klanten_KlantId",
                table: "Bestellingen");

            migrationBuilder.DropIndex(
                name: "IX_Bestellingen_KlantId",
                table: "Bestellingen");

            migrationBuilder.DropColumn(
                name: "KlantId",
                table: "Bestellingen");
        }
    }
}
