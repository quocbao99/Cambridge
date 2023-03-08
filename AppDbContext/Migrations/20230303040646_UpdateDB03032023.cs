using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbContext.Migrations
{
    public partial class UpdateDB03032023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoutDownLoad",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CoutDownLoadMonth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DateDownLoad",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OpenCar",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OpenTruct",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "SocialType",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SocialType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoutDownLoad",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoutDownLoadMonth",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "DateDownLoad",
                table: "Users",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "OpenCar",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OpenTruct",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
