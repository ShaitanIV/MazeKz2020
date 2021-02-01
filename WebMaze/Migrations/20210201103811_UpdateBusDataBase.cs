using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMaze.Migrations
{
    public partial class UpdateBusDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "License",
                table: "BusWorker");

            migrationBuilder.DropColumn(
                name: "Route",
                table: "BusOrder");

            migrationBuilder.AddColumn<long>(
                name: "CertificateId",
                table: "BusWorker",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CitizenUserId",
                table: "BusWorker",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CurrentLocation",
                table: "Bus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentOccupation",
                table: "Bus",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReversedDirection",
                table: "Bus",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusWorker_CertificateId",
                table: "BusWorker",
                column: "CertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_BusWorker_CitizenUserId",
                table: "BusWorker",
                column: "CitizenUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BusWorker_Certificates_CertificateId",
                table: "BusWorker",
                column: "CertificateId",
                principalTable: "Certificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusWorker_CitizenUser_CitizenUserId",
                table: "BusWorker",
                column: "CitizenUserId",
                principalTable: "CitizenUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusWorker_Certificates_CertificateId",
                table: "BusWorker");

            migrationBuilder.DropForeignKey(
                name: "FK_BusWorker_CitizenUser_CitizenUserId",
                table: "BusWorker");

            migrationBuilder.DropIndex(
                name: "IX_BusWorker_CertificateId",
                table: "BusWorker");

            migrationBuilder.DropIndex(
                name: "IX_BusWorker_CitizenUserId",
                table: "BusWorker");

            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "BusWorker");

            migrationBuilder.DropColumn(
                name: "CitizenUserId",
                table: "BusWorker");

            migrationBuilder.DropColumn(
                name: "CurrentLocation",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "CurrentOccupation",
                table: "Bus");

            migrationBuilder.DropColumn(
                name: "ReversedDirection",
                table: "Bus");

            migrationBuilder.AddColumn<string>(
                name: "License",
                table: "BusWorker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Route",
                table: "BusOrder",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
