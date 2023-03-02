using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbContext.Migrations
{
    public partial class EDITMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Link",
                table: "MaterialSub",
                newName: "FileUrl");

            migrationBuilder.AlterColumn<Guid>(
                name: "MaterialID",
                table: "MaterialSub",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "MaterialSub",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Material",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "Material",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "MaterialSub");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "Material");

            migrationBuilder.RenameColumn(
                name: "FileUrl",
                table: "MaterialSub",
                newName: "Link");

            migrationBuilder.AlterColumn<string>(
                name: "MaterialID",
                table: "MaterialSub",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
