using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt3.Migrations
{
    
    public partial class DodanieRelacjiPokojUzytkownik : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Pokoje",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pokoje_UserId",
                table: "Pokoje",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pokoje_AspNetUsers_UserId",
                table: "Pokoje",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokoje_AspNetUsers_UserId",
                table: "Pokoje");

            migrationBuilder.DropIndex(
                name: "IX_Pokoje_UserId",
                table: "Pokoje");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pokoje");
        }
    }
}
