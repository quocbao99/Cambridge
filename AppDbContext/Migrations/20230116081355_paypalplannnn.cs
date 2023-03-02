using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbContext.Migrations
{
    public partial class paypalplannnn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodConfigurationID",
                table: "PlanPaypal",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlanPaypalID",
                table: "PlanPaypal",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethodConfigurationID",
                table: "PlanPaypal");

            migrationBuilder.DropColumn(
                name: "PlanPaypalID",
                table: "PlanPaypal");
        }
    }
}
