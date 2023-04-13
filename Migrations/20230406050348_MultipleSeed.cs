using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dotnet_mvc.Migrations
{
    /// <inheritdoc />
    public partial class MultipleSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 1,
                column: "email",
                value: "arham@gmail.com");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "id", "department", "email", "name" },
                values: new object[,]
                {
                    { 2, 4, "arham@abeer.com", "Abeer" },
                    { 3, 0, "ahmed@aaa.com", "Ahmed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "id",
                keyValue: 1,
                column: "email",
                value: "arham@abeer.com");
        }
    }
}
