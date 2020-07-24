using Microsoft.EntityFrameworkCore.Migrations;

namespace Havit.GoranG3.Entity.Migrations
{
    public partial class ContactRelationship_AddIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContactRelationship_ParentContactId",
                table: "ContactRelationship");

            migrationBuilder.CreateIndex(
                name: "IX_ContactRelationship_ParentContactId_DetailContactId",
                table: "ContactRelationship",
                columns: new[] { "ParentContactId", "DetailContactId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContactRelationship_ParentContactId_DetailContactId",
                table: "ContactRelationship");

            migrationBuilder.CreateIndex(
                name: "IX_ContactRelationship_ParentContactId",
                table: "ContactRelationship",
                column: "ParentContactId");
        }
    }
}
