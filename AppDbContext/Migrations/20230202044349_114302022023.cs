using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbContext.Migrations
{
    public partial class _114302022023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessBranch",
                table: "Package",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AccessBrandIDs",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccessBrandTypes",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessLineOff",
                table: "Package",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AccessLineOffIDs",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessLineOffTime",
                table: "Package",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AccessLineOffTypes",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessMaterial",
                table: "Package",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AccessMaterialIDs",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessMaterialSub",
                table: "Package",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AccessMaterialSubIDs",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccessMaterialSubTypes",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccessMaterialTypes",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessModel",
                table: "Package",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AccessModelIDs",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccessModelTypes",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccessPackageTypes",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LineOffType",
                table: "LineOff",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessBranch",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessBrandIDs",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessBrandTypes",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessLineOff",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessLineOffIDs",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessLineOffTime",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessLineOffTypes",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessMaterial",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessMaterialIDs",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessMaterialSub",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessMaterialSubIDs",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessMaterialSubTypes",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessMaterialTypes",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessModel",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessModelIDs",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessModelTypes",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessPackageTypes",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "LineOffType",
                table: "LineOff");
        }
    }
}
