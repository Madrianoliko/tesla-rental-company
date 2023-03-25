using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeslaRentalCompany.API.Migrations
{
    public partial class dataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CostPerDay", "Model", "Range", "YearOfManufacture" },
                values: new object[] { 1, 300, "X", 400, new DateTime(2020, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CostPerDay", "Model", "Range", "YearOfManufacture" },
                values: new object[] { 2, 150, "Y", 300, new DateTime(2019, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CarId", "EndDate", "IsCanceled", "StartDate", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 4, 20, 13, 45, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2023, 4, 1, 13, 45, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 1, new DateTime(2023, 1, 10, 13, 45, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2023, 1, 1, 13, 45, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 3, 2, new DateTime(2023, 3, 21, 13, 45, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2023, 3, 20, 13, 45, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 4, 2, new DateTime(2023, 3, 15, 13, 45, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2023, 3, 10, 13, 45, 0, 0, DateTimeKind.Unspecified), 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
