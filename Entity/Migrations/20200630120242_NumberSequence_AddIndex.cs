using Microsoft.EntityFrameworkCore.Migrations;

namespace Havit.GoranG3.Entity.Migrations
{
    public partial class NumberSequence_AddIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_NumberSequence_MigrationId",
                table: "NumberSequence",
                column: "MigrationId",
                unique: true,
                filter: "[MigrationId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NumberSequence_MigrationId",
                table: "NumberSequence");
        }
    }
}
