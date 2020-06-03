using Microsoft.EntityFrameworkCore.Migrations;

namespace Havit.GoranG3.Entity.Migrations
{
	public partial class UserMigrationAndIndexes : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<bool>(
				name: "Disabled",
				table: "User",
				nullable: false,
				defaultValue: false);

			migrationBuilder.AddColumn<string>(
				name: "DisplayName",
				table: "User",
				maxLength: 100,
				nullable: true);

			migrationBuilder.AddColumn<int>(
				name: "MigrationId",
				table: "User",
				nullable: true);

			migrationBuilder.CreateIndex(
				name: "IX_User_NormalizedEmail",
				table: "User",
				column: "NormalizedEmail",
				unique: true,
				filter: "Deleted IS NULL");

			migrationBuilder.CreateIndex(
				name: "IX_User_NormalizedUsername",
				table: "User",
				column: "NormalizedUsername",
				unique: true,
				filter: "Deleted IS NULL");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(
				name: "IX_User_NormalizedEmail",
				table: "User");

			migrationBuilder.DropIndex(
				name: "IX_User_NormalizedUsername",
				table: "User");

			migrationBuilder.DropColumn(
				name: "Disabled",
				table: "User");

			migrationBuilder.DropColumn(
				name: "DisplayName",
				table: "User");

			migrationBuilder.DropColumn(
				name: "MigrationId",
				table: "User");
		}
	}
}
