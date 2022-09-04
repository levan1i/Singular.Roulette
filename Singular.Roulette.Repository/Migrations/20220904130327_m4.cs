using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Singular.Roulette.Repository.Migrations
{
    public partial class m4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bets_Spins_SpinId",
                table: "Bets");

            migrationBuilder.AlterColumn<long>(
                name: "SpinId",
                table: "Bets",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Bets_Spins_SpinId",
                table: "Bets",
                column: "SpinId",
                principalTable: "Spins",
                principalColumn: "SpinId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bets_Spins_SpinId",
                table: "Bets");

            migrationBuilder.AlterColumn<long>(
                name: "SpinId",
                table: "Bets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bets_Spins_SpinId",
                table: "Bets",
                column: "SpinId",
                principalTable: "Spins",
                principalColumn: "SpinId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
