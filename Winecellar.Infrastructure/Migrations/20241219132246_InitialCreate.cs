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
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UUID", nullable: false),
                    email = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    username = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    password = table.Column<string>(type: "VARCHAR(60)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

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
                table: "users",
                columns: new[] { "id", "email", "password", "username" },
                values: new object[,]
                {
                    { new Guid("34cc7e90-39de-41f3-b5db-a86b0c4e9008"), "user2@mail.com", "123", "User2" },
                    { new Guid("80967cc2-9bef-4620-8a7d-15e55a1d2231"), "user1@mail.com", "123", "User1" }
                });

            migrationBuilder.InsertData(
                table: "wines",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("9ac1e64a-417f-4a3e-ab83-82478d2331e7"), "Wine 2" },
                    { new Guid("f5b3313d-5336-4663-82a3-dd2287ab6930"), "Wine 1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                table: "users",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_wines_name",
                table: "wines",
                column: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "wines");
        }
    }
}
