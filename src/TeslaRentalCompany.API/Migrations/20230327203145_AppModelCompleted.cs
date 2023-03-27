using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeslaRentalCompany.API.Migrations
{
    public partial class AppModelCompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarDealerships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Localization = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDealerships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarDealershipId = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfManufacture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Range = table.Column<int>(type: "int", nullable: false),
                    DisperseHundreds = table.Column<double>(type: "float", nullable: false),
                    TopSpeed = table.Column<int>(type: "int", nullable: false),
                    HorsePower = table.Column<int>(type: "int", nullable: false),
                    CostPerDay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarDealerships_CarDealershipId",
                        column: x => x.CarDealershipId,
                        principalTable: "CarDealerships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarDealerships",
                columns: new[] { "Id", "Localization" },
                values: new object[,]
                {
                    { 1, "Palma Airport" },
                    { 2, "Palma City Center" },
                    { 3, "Alcudia" },
                    { 4, "Manacor" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "IsAdmin", "LastName", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "Admin", true, "Adminowski", "admin", "admin" },
                    { 2, "Marcin", false, "Marcinowski", "user1", "user1" },
                    { 3, "Jan", false, "Janowski", "user2", "user2" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CarDealershipId", "CostPerDay", "DateOfManufacture", "DisperseHundreds", "HorsePower", "Model", "Range", "TopSpeed" },
                values: new object[,]
                {
                    { 1, 1, 100, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2.1000000000000001, 1020, "S", 600, 322 },
                    { 2, 2, 110, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2.1000000000000001, 1020, "S", 600, 322 },
                    { 3, 3, 80, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3.2999999999999998, 480, "3", 602, 255 },
                    { 4, 4, 120, new DateTime(2022, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3.3999999999999999, 1020, "X", 543, 250 },
                    { 5, 1, 90, new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5.0, 400, "Y", 533, 217 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CarId", "Cost", "EndDate", "IsCanceled", "StartDate", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 0, new DateTime(2023, 5, 20, 13, 45, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2023, 5, 1, 13, 45, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, 1, 0, new DateTime(2023, 3, 20, 13, 45, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2023, 3, 1, 13, 45, 0, 0, DateTimeKind.Unspecified), 3, 1 },
                    { 3, 2, 0, new DateTime(2023, 5, 30, 13, 45, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2023, 5, 15, 13, 45, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 4, 3, 0, new DateTime(2023, 6, 20, 13, 45, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2023, 6, 1, 13, 45, 0, 0, DateTimeKind.Unspecified), 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarDealershipId",
                table: "Cars",
                column: "CarDealershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CarId",
                table: "Reservations",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CarDealerships");
        }
    }
}
