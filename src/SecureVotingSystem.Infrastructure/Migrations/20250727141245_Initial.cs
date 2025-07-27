using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SecureVotingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Voters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActivationCode = table.Column<string>(type: "TEXT", nullable: true),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    IsVoted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Modified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voters", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Voters",
                columns: new[] { "Id", "ActivationCode", "Created", "FullName", "IsVoted", "Modified", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "VOTE001", new DateTime(2025, 7, 27, 14, 12, 43, 545, DateTimeKind.Utc).AddTicks(5051), "Alice Smith", false, new DateTime(2025, 7, 27, 14, 12, 43, 545, DateTimeKind.Utc).AddTicks(5234), "111-222-3333" },
                    { 2, "VOTE002", new DateTime(2025, 7, 27, 14, 12, 43, 545, DateTimeKind.Utc).AddTicks(5393), "Bob Johnson", false, new DateTime(2025, 7, 27, 14, 12, 43, 545, DateTimeKind.Utc).AddTicks(5393), "444-555-6666" },
                    { 3, "VOTE003", new DateTime(2025, 7, 27, 14, 12, 43, 545, DateTimeKind.Utc).AddTicks(5395), "Charlie Brown", true, new DateTime(2025, 7, 27, 14, 12, 43, 545, DateTimeKind.Utc).AddTicks(5395), "777-888-9999" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Voters_ActivationCode",
                table: "Voters",
                column: "ActivationCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Voters");
        }
    }
}
