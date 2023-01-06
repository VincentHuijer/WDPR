using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Auth10Rooster6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rollen_Medewerkers_MedewerkerId",
                table: "Rollen");

            migrationBuilder.DropIndex(
                name: "IX_Rollen_MedewerkerId",
                table: "Rollen");

            migrationBuilder.DropColumn(
                name: "MedewerkerId",
                table: "Rollen");

            migrationBuilder.RenameColumn(
                name: "ContactGegevens",
                table: "Medewerkers",
                newName: "TwoFactorAuthSecretKey");

            migrationBuilder.AddColumn<int>(
                name: "Inlogpoging",
                table: "Medewerkers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Medewerkers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RolNaam",
                table: "Medewerkers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorAuthSetupComplete",
                table: "Medewerkers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Medewerkers_RolNaam",
                table: "Medewerkers",
                column: "RolNaam");

            migrationBuilder.AddForeignKey(
                name: "FK_Medewerkers_Rollen_RolNaam",
                table: "Medewerkers",
                column: "RolNaam",
                principalTable: "Rollen",
                principalColumn: "Naam",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medewerkers_Rollen_RolNaam",
                table: "Medewerkers");

            migrationBuilder.DropIndex(
                name: "IX_Medewerkers_RolNaam",
                table: "Medewerkers");

            migrationBuilder.DropColumn(
                name: "Inlogpoging",
                table: "Medewerkers");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Medewerkers");

            migrationBuilder.DropColumn(
                name: "RolNaam",
                table: "Medewerkers");

            migrationBuilder.DropColumn(
                name: "TwoFactorAuthSetupComplete",
                table: "Medewerkers");

            migrationBuilder.RenameColumn(
                name: "TwoFactorAuthSecretKey",
                table: "Medewerkers",
                newName: "ContactGegevens");

            migrationBuilder.AddColumn<int>(
                name: "MedewerkerId",
                table: "Rollen",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rollen_MedewerkerId",
                table: "Rollen",
                column: "MedewerkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rollen_Medewerkers_MedewerkerId",
                table: "Rollen",
                column: "MedewerkerId",
                principalTable: "Medewerkers",
                principalColumn: "Id");
        }
    }
}
