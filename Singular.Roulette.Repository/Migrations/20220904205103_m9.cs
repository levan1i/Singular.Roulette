using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Singular.Roulette.Repository.Migrations
{
    public partial class m9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Bets_CreateDate",
                table: "Bets",
                column: "CreateDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bets_CreateDate",
                table: "Bets");
        }
    }
}
