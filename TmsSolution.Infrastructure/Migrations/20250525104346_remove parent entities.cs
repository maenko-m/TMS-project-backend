using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TmsSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeparententities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSteps_TestSteps_ParentStepId",
                table: "TestSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSuites_TestSuites_ParentSuiteId",
                table: "TestSuites");

            migrationBuilder.DropIndex(
                name: "IX_TestSuites_ParentSuiteId",
                table: "TestSuites");

            migrationBuilder.DropIndex(
                name: "IX_TestSteps_ParentStepId",
                table: "TestSteps");

            migrationBuilder.DropColumn(
                name: "ParentSuiteId",
                table: "TestSuites");

            migrationBuilder.DropColumn(
                name: "Attachments",
                table: "TestSteps");

            migrationBuilder.DropColumn(
                name: "ParentStepId",
                table: "TestSteps");

            migrationBuilder.DropColumn(
                name: "ExternalIssueId",
                table: "Defects");

            migrationBuilder.AddColumn<Guid>(
                name: "TestStepId",
                table: "Attachments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_TestStepId",
                table: "Attachments",
                column: "TestStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_TestSteps_TestStepId",
                table: "Attachments",
                column: "TestStepId",
                principalTable: "TestSteps",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_TestSteps_TestStepId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_TestStepId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "TestStepId",
                table: "Attachments");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentSuiteId",
                table: "TestSuites",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attachments",
                table: "TestSteps",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentStepId",
                table: "TestSteps",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalIssueId",
                table: "Defects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestSuites_ParentSuiteId",
                table: "TestSuites",
                column: "ParentSuiteId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSteps_ParentStepId",
                table: "TestSteps",
                column: "ParentStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestSteps_TestSteps_ParentStepId",
                table: "TestSteps",
                column: "ParentStepId",
                principalTable: "TestSteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSuites_TestSuites_ParentSuiteId",
                table: "TestSuites",
                column: "ParentSuiteId",
                principalTable: "TestSuites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
