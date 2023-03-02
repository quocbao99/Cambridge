using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbContext.Migrations
{
    public partial class ghfhfhffhfhfh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppStoreProductID",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppStoreSubscriptionID",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSendMail",
                table: "Notification",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppStoreProductID",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "AppStoreSubscriptionID",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "IsSendMail",
                table: "Notification");
        }
    }
}
