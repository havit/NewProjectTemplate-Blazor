using Microsoft.EntityFrameworkCore.Migrations;

namespace Havit.GoranG3.Entity.Migrations
{
    public partial class AbsenceTypeDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBalance",
                table: "AbsenceType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AbsenceType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "AbsenceType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AbsenceType",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UiOrder",
                table: "AbsenceType",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBalance",
                table: "AbsenceType");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AbsenceType");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "AbsenceType");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AbsenceType");

            migrationBuilder.DropColumn(
                name: "UiOrder",
                table: "AbsenceType");
        }
    }
}
