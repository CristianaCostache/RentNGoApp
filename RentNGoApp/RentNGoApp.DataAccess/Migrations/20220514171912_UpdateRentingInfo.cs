using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentNGoApp.DataAccess.Migrations
{
    public partial class UpdateRentingInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userGuid",
                table: "rentingInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userGuid",
                table: "rentingInfos");
        }
    }
}
