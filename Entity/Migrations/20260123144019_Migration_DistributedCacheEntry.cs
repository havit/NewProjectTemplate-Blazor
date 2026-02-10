using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Havit.NewProjectTemplate.Entity.Migrations
{
    /// <inheritdoc />
    public partial class Migration_DistributedCacheEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DistributedCacheEntry",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ExpiresAtTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    SlidingExpirationInSeconds = table.Column<long>(type: "bigint", nullable: true),
                    AbsoluteExpiration = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributedCacheEntry", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistributedCacheEntry");
        }
    }
}
