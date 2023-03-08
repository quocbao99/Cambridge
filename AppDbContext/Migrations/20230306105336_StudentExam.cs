using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbContext.Migrations
{
    public partial class StudentExam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamRoomStatus",
                table: "ExaminationRoom");

            migrationBuilder.AddColumn<Guid>(
                name: "ExamContentID",
                table: "ExaminationRoom",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SupervisorID",
                table: "ExaminationRoom",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudentExam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    isVertified = table.Column<bool>(type: "bit", nullable: true),
                    isOnline = table.Column<bool>(type: "bit", nullable: true),
                    Created = table.Column<double>(type: "float", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Updated = table.Column<double>(type: "float", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExam", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentExam");

            migrationBuilder.DropColumn(
                name: "ExamContentID",
                table: "ExaminationRoom");

            migrationBuilder.DropColumn(
                name: "SupervisorID",
                table: "ExaminationRoom");

            migrationBuilder.AddColumn<int>(
                name: "ExamRoomStatus",
                table: "ExaminationRoom",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
