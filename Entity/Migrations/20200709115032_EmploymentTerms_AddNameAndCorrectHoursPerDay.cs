using Microsoft.EntityFrameworkCore.Migrations;

namespace Havit.GoranG3.Entity.Migrations
{
    public partial class EmploymentTerms_AddNameAndCorrectHoursPerDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HourPerDay",
                table: "EmploymentTerms",
                newName: "HoursPerDay");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EmploymentTerms",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "EmploymentTerms");

            migrationBuilder.RenameColumn(
                name: "HoursPerDay",
                table: "EmploymentTerms",
                newName: "HourPerDay");
        }
    }
}
