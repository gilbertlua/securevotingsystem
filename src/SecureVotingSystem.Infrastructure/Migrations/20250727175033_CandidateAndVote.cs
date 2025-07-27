using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SecureVotingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CandidateAndVote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ActivationCode",
                table: "Voters",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Modified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VoterId = table.Column<int>(type: "INTEGER", nullable: false),
                    CandidateId = table.Column<int>(type: "INTEGER", nullable: false),
                    VoteTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_Voters_VoterId",
                        column: x => x.VoterId,
                        principalTable: "Voters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "Created", "Modified", "Name" },
                values: new object[,]
                {
                    { 101, new DateTime(2025, 7, 27, 17, 50, 32, 936, DateTimeKind.Utc).AddTicks(334), new DateTime(2025, 7, 27, 17, 50, 32, 936, DateTimeKind.Utc).AddTicks(514), "Candidate A" },
                    { 102, new DateTime(2025, 7, 27, 17, 50, 32, 936, DateTimeKind.Utc).AddTicks(668), new DateTime(2025, 7, 27, 17, 50, 32, 936, DateTimeKind.Utc).AddTicks(669), "Candidate B" },
                    { 103, new DateTime(2025, 7, 27, 17, 50, 32, 936, DateTimeKind.Utc).AddTicks(670), new DateTime(2025, 7, 27, 17, 50, 32, 936, DateTimeKind.Utc).AddTicks(671), "Candidate C" }
                });

            migrationBuilder.UpdateData(
                table: "Voters",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2025, 7, 27, 17, 50, 32, 935, DateTimeKind.Utc).AddTicks(1844), new DateTime(2025, 7, 27, 17, 50, 32, 935, DateTimeKind.Utc).AddTicks(2022) });

            migrationBuilder.UpdateData(
                table: "Voters",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2025, 7, 27, 17, 50, 32, 935, DateTimeKind.Utc).AddTicks(2175), new DateTime(2025, 7, 27, 17, 50, 32, 935, DateTimeKind.Utc).AddTicks(2175) });

            migrationBuilder.UpdateData(
                table: "Voters",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2025, 7, 27, 17, 50, 32, 935, DateTimeKind.Utc).AddTicks(2177), new DateTime(2025, 7, 27, 17, 50, 32, 935, DateTimeKind.Utc).AddTicks(2178) });

            migrationBuilder.InsertData(
                table: "Votes",
                columns: new[] { "Id", "CandidateId", "VoteTimestamp", "VoterId" },
                values: new object[,]
                {
                    { 1, 101, new DateTime(2025, 7, 27, 17, 40, 32, 939, DateTimeKind.Utc).AddTicks(5406), 1 },
                    { 2, 102, new DateTime(2025, 7, 27, 17, 45, 32, 939, DateTimeKind.Utc).AddTicks(5682), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CandidateId",
                table: "Votes",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_VoterId",
                table: "Votes",
                column: "VoterId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.AlterColumn<string>(
                name: "ActivationCode",
                table: "Voters",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.UpdateData(
                table: "Voters",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2025, 7, 27, 14, 12, 43, 545, DateTimeKind.Utc).AddTicks(5051), new DateTime(2025, 7, 27, 14, 12, 43, 545, DateTimeKind.Utc).AddTicks(5234) });

            migrationBuilder.UpdateData(
                table: "Voters",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2025, 7, 27, 14, 12, 43, 545, DateTimeKind.Utc).AddTicks(5393), new DateTime(2025, 7, 27, 14, 12, 43, 545, DateTimeKind.Utc).AddTicks(5393) });

            migrationBuilder.UpdateData(
                table: "Voters",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "Modified" },
                values: new object[] { new DateTime(2025, 7, 27, 14, 12, 43, 545, DateTimeKind.Utc).AddTicks(5395), new DateTime(2025, 7, 27, 14, 12, 43, 545, DateTimeKind.Utc).AddTicks(5395) });
        }
    }
}
