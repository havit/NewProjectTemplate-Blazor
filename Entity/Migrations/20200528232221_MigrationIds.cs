using Microsoft.EntityFrameworkCore.Migrations;

namespace Havit.GoranG3.Entity.Migrations
{
    public partial class MigrationIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "TransactionItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "TimesheetItemCategory",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "TimesheetItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "Team",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "ProjectPhase",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "Payment",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "NumberSequence",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "ExchangeRate",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "EmployeeHistory",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "Employee",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "Currency",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "BusinessCase",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "BankAccount",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "AttridaDocument",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "AttridaComment",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MigrationId",
                table: "Absence",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "TimesheetItemCategory");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "TimesheetItem");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "ProjectPhase");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "NumberSequence");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "ExchangeRate");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "EmployeeHistory");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "BusinessCase");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "AttridaDocument");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "AttridaComment");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "MigrationId",
                table: "Absence");
        }
    }
}
