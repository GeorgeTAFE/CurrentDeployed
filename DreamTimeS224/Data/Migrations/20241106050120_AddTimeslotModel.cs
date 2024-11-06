using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamTimeS224.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeslotModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Timeslots",
                columns: table => new
                {
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timeslots", x => x.Time);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timeslots");
        }
    }
}
