using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Auth9Rooster13Groepen1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActeurVoorstellingen_Show_ShowId",
                table: "ActeurVoorstellingen");

            migrationBuilder.DropForeignKey(
                name: "FK_Kaartjeshouders_Show_ShowId",
                table: "Kaartjeshouders");

            migrationBuilder.DropForeignKey(
                name: "FK_Show_ArtiestGroepen_ArtiestGroepId",
                table: "Show");

            migrationBuilder.DropForeignKey(
                name: "FK_Show_Kalenders_KalenderId",
                table: "Show");

            migrationBuilder.DropForeignKey(
                name: "FK_Show_Voorstellingen_VoorstellingId",
                table: "Show");

            migrationBuilder.DropForeignKey(
                name: "FK_Show_Zalen_Zaalnummer",
                table: "Show");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Show",
                table: "Show");

            migrationBuilder.RenameTable(
                name: "Show",
                newName: "Shows");

            migrationBuilder.RenameIndex(
                name: "IX_Show_Zaalnummer",
                table: "Shows",
                newName: "IX_Shows_Zaalnummer");

            migrationBuilder.RenameIndex(
                name: "IX_Show_VoorstellingId",
                table: "Shows",
                newName: "IX_Shows_VoorstellingId");

            migrationBuilder.RenameIndex(
                name: "IX_Show_KalenderId",
                table: "Shows",
                newName: "IX_Shows_KalenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Show_ArtiestGroepId",
                table: "Shows",
                newName: "IX_Shows_ArtiestGroepId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shows",
                table: "Shows",
                column: "ShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActeurVoorstellingen_Shows_ShowId",
                table: "ActeurVoorstellingen",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "ShowId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Kaartjeshouders_Shows_ShowId",
                table: "Kaartjeshouders",
                column: "ShowId",
                principalTable: "Shows",
                principalColumn: "ShowId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_ArtiestGroepen_ArtiestGroepId",
                table: "Shows",
                column: "ArtiestGroepId",
                principalTable: "ArtiestGroepen",
                principalColumn: "GroepsId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Kalenders_KalenderId",
                table: "Shows",
                column: "KalenderId",
                principalTable: "Kalenders",
                principalColumn: "KalenderId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Voorstellingen_VoorstellingId",
                table: "Shows",
                column: "VoorstellingId",
                principalTable: "Voorstellingen",
                principalColumn: "VoorstellingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Zalen_Zaalnummer",
                table: "Shows",
                column: "Zaalnummer",
                principalTable: "Zalen",
                principalColumn: "Zaalnummer",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActeurVoorstellingen_Shows_ShowId",
                table: "ActeurVoorstellingen");

            migrationBuilder.DropForeignKey(
                name: "FK_Kaartjeshouders_Shows_ShowId",
                table: "Kaartjeshouders");

            migrationBuilder.DropForeignKey(
                name: "FK_Shows_ArtiestGroepen_ArtiestGroepId",
                table: "Shows");

            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Kalenders_KalenderId",
                table: "Shows");

            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Voorstellingen_VoorstellingId",
                table: "Shows");

            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Zalen_Zaalnummer",
                table: "Shows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shows",
                table: "Shows");

            migrationBuilder.RenameTable(
                name: "Shows",
                newName: "Show");

            migrationBuilder.RenameIndex(
                name: "IX_Shows_Zaalnummer",
                table: "Show",
                newName: "IX_Show_Zaalnummer");

            migrationBuilder.RenameIndex(
                name: "IX_Shows_VoorstellingId",
                table: "Show",
                newName: "IX_Show_VoorstellingId");

            migrationBuilder.RenameIndex(
                name: "IX_Shows_KalenderId",
                table: "Show",
                newName: "IX_Show_KalenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Shows_ArtiestGroepId",
                table: "Show",
                newName: "IX_Show_ArtiestGroepId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Show",
                table: "Show",
                column: "ShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActeurVoorstellingen_Show_ShowId",
                table: "ActeurVoorstellingen",
                column: "ShowId",
                principalTable: "Show",
                principalColumn: "ShowId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Kaartjeshouders_Show_ShowId",
                table: "Kaartjeshouders",
                column: "ShowId",
                principalTable: "Show",
                principalColumn: "ShowId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Show_ArtiestGroepen_ArtiestGroepId",
                table: "Show",
                column: "ArtiestGroepId",
                principalTable: "ArtiestGroepen",
                principalColumn: "GroepsId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Show_Kalenders_KalenderId",
                table: "Show",
                column: "KalenderId",
                principalTable: "Kalenders",
                principalColumn: "KalenderId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Show_Voorstellingen_VoorstellingId",
                table: "Show",
                column: "VoorstellingId",
                principalTable: "Voorstellingen",
                principalColumn: "VoorstellingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Show_Zalen_Zaalnummer",
                table: "Show",
                column: "Zaalnummer",
                principalTable: "Zalen",
                principalColumn: "Zaalnummer",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
