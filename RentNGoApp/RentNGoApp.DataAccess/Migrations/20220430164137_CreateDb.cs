using Microsoft.EntityFrameworkCore.Migrations;
using System;


#nullable disable

namespace RentNGoApp.DataAccess.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    carId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.carId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    imageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    carId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.imageId);
                    table.ForeignKey(
                        name: "FK_images_cars_carId",
                        column: x => x.carId,
                        principalTable: "cars",
                        principalColumn: "carId");
                });

            migrationBuilder.CreateTable(
                name: "rentingInfos",
                columns: table => new
                {
                    rentingInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    carId = table.Column<int>(type: "int", nullable: false),
                    rentingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rentingInfos", x => x.rentingInfoId);
                    table.ForeignKey(
                        name: "FK_rentingInfos_cars_carId",
                        column: x => x.carId,
                        principalTable: "cars",
                        principalColumn: "carId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rentingInfos_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_images_carId",
                table: "images",
                column: "carId");

            migrationBuilder.CreateIndex(
                name: "IX_rentingInfos_carId",
                table: "rentingInfos",
                column: "carId");

            migrationBuilder.CreateIndex(
                name: "IX_rentingInfos_userId",
                table: "rentingInfos",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "rentingInfos");

            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
