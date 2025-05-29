using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TmsSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Someedits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestRuns_Users_AssignedToId",
                table: "TestRuns");

            migrationBuilder.DropForeignKey(
                name: "FK_TestRunTestCases_Users_AssignedToId",
                table: "TestRunTestCases");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "CustomFields",
                table: "TestRuns");

            migrationBuilder.DropColumn(
                name: "ExternalIssueId",
                table: "TestRuns");

            migrationBuilder.RenameColumn(
                name: "AssignedToId",
                table: "TestRunTestCases",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TestRunTestCases_AssignedToId",
                table: "TestRunTestCases",
                newName: "IX_TestRunTestCases_UserId");

            migrationBuilder.RenameColumn(
                name: "AssignedToId",
                table: "TestRuns",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TestRuns_AssignedToId",
                table: "TestRuns",
                newName: "IX_TestRuns_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestRuns_Users_UserId",
                table: "TestRuns",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestRunTestCases_Users_UserId",
                table: "TestRunTestCases",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestRuns_Users_UserId",
                table: "TestRuns");

            migrationBuilder.DropForeignKey(
                name: "FK_TestRunTestCases_Users_UserId",
                table: "TestRunTestCases");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TestRunTestCases",
                newName: "AssignedToId");

            migrationBuilder.RenameIndex(
                name: "IX_TestRunTestCases_UserId",
                table: "TestRunTestCases",
                newName: "IX_TestRunTestCases_AssignedToId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TestRuns",
                newName: "AssignedToId");

            migrationBuilder.RenameIndex(
                name: "IX_TestRuns_UserId",
                table: "TestRuns",
                newName: "IX_TestRuns_AssignedToId");

            migrationBuilder.AddColumn<string>(
                name: "CustomFields",
                table: "TestRuns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalIssueId",
                table: "TestRuns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_ProjectId",
                table: "AuditLogs",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestRuns_Users_AssignedToId",
                table: "TestRuns",
                column: "AssignedToId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestRunTestCases_Users_AssignedToId",
                table: "TestRunTestCases",
                column: "AssignedToId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
