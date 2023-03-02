using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbContext.Migrations
{
    public partial class VNPay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "PaymentMethodType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Command",
                table: "PaymentMethodConfiguration",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrCode",
                table: "PaymentMethodConfiguration",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Locale",
                table: "PaymentMethodConfiguration",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "paymentVNPayId",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "PaymentMethodType");

            migrationBuilder.DropColumn(
                name: "Command",
                table: "PaymentMethodConfiguration");

            migrationBuilder.DropColumn(
                name: "CurrCode",
                table: "PaymentMethodConfiguration");

            migrationBuilder.DropColumn(
                name: "Locale",
                table: "PaymentMethodConfiguration");

            migrationBuilder.DropColumn(
                name: "paymentVNPayId",
                table: "Payment");
        }
    }
}
