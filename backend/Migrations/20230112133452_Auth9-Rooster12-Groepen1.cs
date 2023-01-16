using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Auth9Rooster12Groepen1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BesteldeStoel_Bestellingen_BestellingId",
                table: "BesteldeStoel");

            migrationBuilder.DropForeignKey(
                name: "FK_BesteldeStoel_Stoel_StoelID",
                table: "BesteldeStoel");

            migrationBuilder.DropForeignKey(
                name: "FK_Stoel_Zalen_Zaalnummer",
                table: "Stoel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stoel",
                table: "Stoel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BesteldeStoel",
                table: "BesteldeStoel");

            migrationBuilder.RenameTable(
                name: "Stoel",
                newName: "Stoelen");

            migrationBuilder.RenameTable(
                name: "BesteldeStoel",
                newName: "BesteldeStoelen");

            migrationBuilder.RenameIndex(
                name: "IX_Stoel_Zaalnummer",
                table: "Stoelen",
                newName: "IX_Stoelen_Zaalnummer");

            migrationBuilder.RenameIndex(
                name: "IX_BesteldeStoel_BestellingId",
                table: "BesteldeStoelen",
                newName: "IX_BesteldeStoelen_BestellingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stoelen",
                table: "Stoelen",
                column: "StoelID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BesteldeStoelen",
                table: "BesteldeStoelen",
                columns: new[] { "StoelID", "BestellingId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BesteldeStoelen_Bestellingen_BestellingId",
                table: "BesteldeStoelen",
                column: "BestellingId",
                principalTable: "Bestellingen",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_BesteldeStoelen_Stoelen_StoelID",
                table: "BesteldeStoelen",
                column: "StoelID",
                principalTable: "Stoelen",
                principalColumn: "StoelID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Stoelen_Zalen_Zaalnummer",
                table: "Stoelen",
                column: "Zaalnummer",
                principalTable: "Zalen",
                principalColumn: "Zaalnummer",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BesteldeStoelen_Bestellingen_BestellingId",
                table: "BesteldeStoelen");

            migrationBuilder.DropForeignKey(
                name: "FK_BesteldeStoelen_Stoelen_StoelID",
                table: "BesteldeStoelen");

            migrationBuilder.DropForeignKey(
                name: "FK_Stoelen_Zalen_Zaalnummer",
                table: "Stoelen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stoelen",
                table: "Stoelen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BesteldeStoelen",
                table: "BesteldeStoelen");

            migrationBuilder.RenameTable(
                name: "Stoelen",
                newName: "Stoel");

            migrationBuilder.RenameTable(
                name: "BesteldeStoelen",
                newName: "BesteldeStoel");

            migrationBuilder.RenameIndex(
                name: "IX_Stoelen_Zaalnummer",
                table: "Stoel",
                newName: "IX_Stoel_Zaalnummer");

            migrationBuilder.RenameIndex(
                name: "IX_BesteldeStoelen_BestellingId",
                table: "BesteldeStoel",
                newName: "IX_BesteldeStoel_BestellingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stoel",
                table: "Stoel",
                column: "StoelID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BesteldeStoel",
                table: "BesteldeStoel",
                columns: new[] { "StoelID", "BestellingId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BesteldeStoel_Bestellingen_BestellingId",
                table: "BesteldeStoel",
                column: "BestellingId",
                principalTable: "Bestellingen",
                principalColumn: "BestellingId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_BesteldeStoel_Stoel_StoelID",
                table: "BesteldeStoel",
                column: "StoelID",
                principalTable: "Stoel",
                principalColumn: "StoelID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Stoel_Zalen_Zaalnummer",
                table: "Stoel",
                column: "Zaalnummer",
                principalTable: "Zalen",
                principalColumn: "Zaalnummer",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
