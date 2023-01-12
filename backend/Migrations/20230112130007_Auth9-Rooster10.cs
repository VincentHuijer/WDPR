using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Auth9Rooster10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestelDatum",
                table: "Bestellingen");

            migrationBuilder.RenameColumn(
                name: "bestelDatum",
                table: "Bestellingen",
                newName: "BestelDatum");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BestelDatum",
                table: "Bestellingen",
                newName: "bestelDatum");

            migrationBuilder.AddColumn<DateTime>(
                name: "BestelDatum",
                table: "Bestellingen",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
