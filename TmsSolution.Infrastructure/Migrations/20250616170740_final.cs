using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TmsSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestCaseSharedSteps");

            migrationBuilder.DropTable(
                name: "SharedSteps");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SharedSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpectedResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedSteps_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharedSteps_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestCaseSharedSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SharedStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestCaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCaseSharedSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCaseSharedSteps_SharedSteps_SharedStepId",
                        column: x => x.SharedStepId,
                        principalTable: "SharedSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestCaseSharedSteps_TestCases_TestCaseId",
                        column: x => x.TestCaseId,
                        principalTable: "TestCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SharedSteps_CreatedById",
                table: "SharedSteps",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SharedSteps_ProjectId",
                table: "SharedSteps",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCaseSharedSteps_SharedStepId",
                table: "TestCaseSharedSteps",
                column: "SharedStepId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCaseSharedSteps_TestCaseId",
                table: "TestCaseSharedSteps",
                column: "TestCaseId");
        }
    }
}
