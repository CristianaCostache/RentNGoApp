using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentNGoApp.DataAccess.Migrations
{
    public partial class UpdateCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userGuid",
                table: "cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userGuid",
                table: "cars");
        }
    }
}
