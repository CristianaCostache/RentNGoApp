using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentNGoApp.DataAccess.Migrations
{
    public partial class UpdateRentingInfo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rentingInfos_users_userId",
                table: "rentingInfos");

            migrationBuilder.DropIndex(
                name: "IX_rentingInfos_userId",
                table: "rentingInfos");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "rentingInfos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "rentingInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_rentingInfos_userId",
                table: "rentingInfos",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_rentingInfos_users_userId",
                table: "rentingInfos",
                column: "userId",
                principalTable: "users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
