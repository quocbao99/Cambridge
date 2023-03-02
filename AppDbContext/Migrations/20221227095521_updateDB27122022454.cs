using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbContext.Migrations
{
    public partial class updateDB27122022454 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Solution",
                table: "DTC",
                newName: "ReferenceLocation");

            migrationBuilder.RenameColumn(
                name: "ScopeOnVehicle",
                table: "DTC",
                newName: "ReferenceCircuitDiagram");

            migrationBuilder.RenameColumn(
                name: "ErrorContent",
                table: "DTC",
                newName: "PossibleCause");

            migrationBuilder.AddColumn<int>(
                name: "StatusForSubScription",
                table: "Payment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DTC",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DTC",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymenntID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<double>(type: "float", nullable: false),
                    EndTime = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<double>(type: "float", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Updated = table.Column<double>(type: "float", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropColumn(
                name: "StatusForSubScription",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DTC");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DTC");

            migrationBuilder.RenameColumn(
                name: "ReferenceLocation",
                table: "DTC",
                newName: "Solution");

            migrationBuilder.RenameColumn(
                name: "ReferenceCircuitDiagram",
                table: "DTC",
                newName: "ScopeOnVehicle");

            migrationBuilder.RenameColumn(
                name: "PossibleCause",
                table: "DTC",
                newName: "ErrorContent");
        }
    }
}
