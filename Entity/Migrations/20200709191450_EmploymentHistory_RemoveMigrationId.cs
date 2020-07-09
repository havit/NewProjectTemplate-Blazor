using Microsoft.EntityFrameworkCore.Migrations;

namespace Havit.GoranG3.Entity.Migrations
{
    public partial class EmploymentHistory_RemoveMigrationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "EmployeeHistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "EmployeeHistory",
                type: "int",
                nullable: true);
        }
    }
}
