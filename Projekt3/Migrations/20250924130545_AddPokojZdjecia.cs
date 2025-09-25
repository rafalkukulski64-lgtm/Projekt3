using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt3.Migrations
{
    public partial class AddPokojZdjecia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokojZdjecia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazwaPliku = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SciezkaPliku = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataDodania = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PokojId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokojZdjecia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokojZdjecia_Pokoje_PokojId",
                        column: x => x.PokojId,
                        principalTable: "Pokoje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokojZdjecia_PokojId",
                table: "PokojZdjecia",
                column: "PokojId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokojZdjecia");
        }
    }
}
