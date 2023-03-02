using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbContext.Migrations
{
    public partial class ChangePayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "paymentSession");

            migrationBuilder.DropColumn(
                name: "CallbackToken",
                table: "paymentSession");

            migrationBuilder.AddColumn<bool>(
                name: "IsTrial",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CallbackToken",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTrial",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "CallbackToken",
                table: "Payment");

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "paymentSession",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CallbackToken",
                table: "paymentSession",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
