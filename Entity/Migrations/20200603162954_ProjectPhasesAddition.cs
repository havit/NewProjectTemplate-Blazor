using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Havit.GoranG3.Entity.Migrations
{
	public partial class ProjectPhasesAddition : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<DateTime>(
				name: "Created",
				table: "ProjectPhase",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

			migrationBuilder.AddColumn<DateTime>(
				name: "Deleted",
				table: "ProjectPhase",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "Kod",
				table: "ProjectPhase",
				maxLength: 20,
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<string>(
				name: "Nazev",
				table: "ProjectPhase",
				maxLength: 100,
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<int>(
				name: "UiOrder",
				table: "ProjectPhase",
				nullable: false,
				defaultValue: 0);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Created",
				table: "ProjectPhase");

			migrationBuilder.DropColumn(
				name: "Deleted",
				table: "ProjectPhase");

			migrationBuilder.DropColumn(
				name: "Kod",
				table: "ProjectPhase");

			migrationBuilder.DropColumn(
				name: "Nazev",
				table: "ProjectPhase");

			migrationBuilder.DropColumn(
				name: "UiOrder",
				table: "ProjectPhase");
		}
	}
}
