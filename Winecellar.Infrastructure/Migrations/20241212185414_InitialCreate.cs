using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Winecellar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wines",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UUID", nullable: false),
                    name = table.Column<string>(type: "VARCHAR(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wines", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "wines",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("9ac1e64a-417f-4a3e-ab83-82478d2331e7"), "Wine 2" },
                    { new Guid("9bc1e64a-417f-4a3e-ab83-82478d2331e8"), "Wine 2" },
                    { new Guid("f5b3313d-5336-4663-82a3-dd2287ab6930"), "Wine 1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_wines_name",
                table: "wines",
                column: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wines");
        }
    }
}
