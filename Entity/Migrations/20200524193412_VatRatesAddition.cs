using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Havit.GoranG3.Entity.Migrations
{
	public partial class VatRatesAddition : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "VatRate",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false),
					Rate = table.Column<decimal>(type: "decimal(9, 5)", nullable: false),
					ValidFrom = table.Column<DateTime>(type: "date", nullable: true),
					ValidTo = table.Column<DateTime>(type: "date", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_VatRate", x => x.Id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "VatRate");
		}
	}
}
