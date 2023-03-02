using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbContext.Migrations
{
    public partial class car : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BrandID",
                table: "EModel",
                newName: "CarID");

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "HangfireManage",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "HangfireManage");

            migrationBuilder.RenameColumn(
                name: "CarID",
                table: "EModel",
                newName: "BrandID");
        }
    }
}
