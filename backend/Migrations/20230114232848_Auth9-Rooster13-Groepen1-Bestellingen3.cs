using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Auth9Rooster13Groepen1Bestellingen3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BesteldeStoelen",
                table: "BesteldeStoelen");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BesteldeStoelen",
                table: "BesteldeStoelen",
                columns: new[] { "StoelID", "BestellingId", "Datum" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BesteldeStoelen",
                table: "BesteldeStoelen");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BesteldeStoelen",
                table: "BesteldeStoelen",
                columns: new[] { "StoelID", "BestellingId" });
        }
    }
}
