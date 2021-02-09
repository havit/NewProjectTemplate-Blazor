using Microsoft.EntityFrameworkCore.Migrations;

namespace Havit.GoranG3.Entity.Migrations
{
    public partial class ProjectPhaseNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kod",
                table: "ProjectPhase");

            migrationBuilder.DropColumn(
                name: "Nazev",
                table: "ProjectPhase");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ProjectPhase",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProjectPhase",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "ProjectPhase");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProjectPhase");

            migrationBuilder.AddColumn<string>(
                name: "Kod",
                table: "ProjectPhase",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nazev",
                table: "ProjectPhase",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
