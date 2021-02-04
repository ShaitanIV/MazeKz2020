using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMaze.Migrations
{
    public partial class FixedBusDbNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bus_BusWorker_WorkerId",
                table: "Bus");

            migrationBuilder.DropIndex(
                name: "IX_Bus_WorkerId",
                table: "Bus");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "Bus",
                newName: "BusWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_BusWorkerId",
                table: "Bus",
                column: "BusWorkerId",
                unique: true,
                filter: "[BusWorkerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_BusWorker_BusWorkerId",
                table: "Bus",
                column: "BusWorkerId",
                principalTable: "BusWorker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bus_BusWorker_BusWorkerId",
                table: "Bus");

            migrationBuilder.DropIndex(
                name: "IX_Bus_BusWorkerId",
                table: "Bus");

            migrationBuilder.RenameColumn(
                name: "BusWorkerId",
                table: "Bus",
                newName: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_WorkerId",
                table: "Bus",
                column: "WorkerId",
                unique: true,
                filter: "[WorkerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Bus_BusWorker_WorkerId",
                table: "Bus",
                column: "WorkerId",
                principalTable: "BusWorker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
