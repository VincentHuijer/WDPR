using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Auth9Rooster13Groepen1Bestellingen3Donaties1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donaties",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Hoeveelheid = table.Column<double>(type: "double precision", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Naam = table.Column<string>(type: "text", nullable: false),
                    Datum = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    KlantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donaties", x => x.id);
                    table.ForeignKey(
                        name: "FK_Donaties_Klanten_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Klanten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donaties_KlantId",
                table: "Donaties",
                column: "KlantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donaties");
        }
    }
}
