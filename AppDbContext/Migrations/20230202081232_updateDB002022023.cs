using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbContext.Migrations
{
    public partial class updateDB002022023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccessBranch",
                table: "Package",
                newName: "AccessDTC");

            migrationBuilder.AddColumn<int>(
                name: "AccessBrand",
                table: "Package",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AccessDTCIDs",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccessDTCTypes",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmodelType",
                table: "EModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessBrand",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessDTCIDs",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AccessDTCTypes",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "EmodelType",
                table: "EModel");

            migrationBuilder.RenameColumn(
                name: "AccessDTC",
                table: "Package",
                newName: "AccessBranch");
        }
    }
}
