using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Singular.Roulette.Repository.Migrations
{
    public partial class m5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isFinnished",
                table: "Bets",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isFinnished",
                table: "Bets");
        }
    }
}
