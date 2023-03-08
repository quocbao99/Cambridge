using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDbContext.Migrations
{
    public partial class addProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<double>(type: "float", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    TypeOfIdentification = table.Column<int>(type: "int", nullable: false),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfIssue = table.Column<double>(type: "float", nullable: false),
                    PlaceOfIssue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecificAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceOfWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoOfCandidate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoOfIDCardOrPassport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurposeOfTakingExam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PointTarget = table.Column<int>(type: "int", nullable: false),
                    TimeOfSubmitingCandidateProfile = table.Column<double>(type: "float", nullable: false),
                    NeedsHardCopyCertification = table.Column<bool>(type: "bit", nullable: true),
                    Created = table.Column<double>(type: "float", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Updated = table.Column<double>(type: "float", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProfile", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentProfile");
        }
    }
}
