using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DreamTimeS224.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataForRoomAndRoomType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "Capacity", "Name" },
                values: new object[,]
                {
                    { 1, 130, "Arcadia" },
                    { 2, 3, "Monstrocity" },
                    { 3, 30, "Book Worms" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Code", "RoomTypeId" },
                values: new object[,]
                {
                    { "A01", 1 },
                    { "A02", 1 },
                    { "A03", 1 },
                    { "M01", 2 },
                    { "M02", 2 },
                    { "M03", 2 },
                    { "M04", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "A01");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "A02");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "A03");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "M01");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "M02");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "M03");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "M04");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
