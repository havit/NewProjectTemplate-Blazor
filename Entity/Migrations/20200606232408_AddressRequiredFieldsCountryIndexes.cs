using Microsoft.EntityFrameworkCore.Migrations;

namespace Havit.GoranG3.Entity.Migrations
{
	public partial class AddressRequiredFieldsCountryIndexes : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Zip",
				table: "Address",
				maxLength: 20,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(20)",
				oldMaxLength: 20);

			migrationBuilder.AlterColumn<string>(
				name: "Line2",
				table: "Address",
				maxLength: 200,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(200)",
				oldMaxLength: 200);

			migrationBuilder.AlterColumn<string>(
				name: "City",
				table: "Address",
				maxLength: 200,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(200)",
				oldMaxLength: 200);

			migrationBuilder.CreateIndex(
				name: "IX_Country_IsoCode",
				table: "Country",
				column: "IsoCode",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Country_IsoCode3",
				table: "Country",
				column: "IsoCode3",
				unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(
				name: "IX_Country_IsoCode",
				table: "Country");

			migrationBuilder.DropIndex(
				name: "IX_Country_IsoCode3",
				table: "Country");

			migrationBuilder.AlterColumn<string>(
				name: "Zip",
				table: "Address",
				type: "nvarchar(20)",
				maxLength: 20,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 20,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "Line2",
				table: "Address",
				type: "nvarchar(200)",
				maxLength: 200,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 200,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "City",
				table: "Address",
				type: "nvarchar(200)",
				maxLength: 200,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 200,
				oldNullable: true);
		}
	}
}
