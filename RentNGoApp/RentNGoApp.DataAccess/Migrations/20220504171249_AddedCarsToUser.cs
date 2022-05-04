using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentNGoApp.DataAccess.Migrations
{
    public partial class AddedCarsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cars_userId",
                table: "cars",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_cars_users_userId",
                table: "cars",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cars_users_userId",
                table: "cars");

            migrationBuilder.DropIndex(
                name: "IX_cars_userId",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "cars");
        }
    }
}
