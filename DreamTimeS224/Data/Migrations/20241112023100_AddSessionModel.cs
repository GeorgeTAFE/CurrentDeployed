using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamTimeS224.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionTypeId = table.Column<int>(type: "int", nullable: false),
                    StartTimeId = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTimeId = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => new { x.Date, x.SessionTypeId });
                    table.ForeignKey(
                        name: "FK_Sessions_SessionTypes_SessionTypeId",
                        column: x => x.SessionTypeId,
                        principalTable: "SessionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessions_Timeslots_EndTimeId",
                        column: x => x.EndTimeId,
                        principalTable: "Timeslots",
                        principalColumn: "Time",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessions_Timeslots_StartTimeId",
                        column: x => x.StartTimeId,
                        principalTable: "Timeslots",
                        principalColumn: "Time",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_EndTimeId",
                table: "Sessions",
                column: "EndTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SessionTypeId",
                table: "Sessions",
                column: "SessionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_StartTimeId",
                table: "Sessions",
                column: "StartTimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessions");
        }
    }
}
