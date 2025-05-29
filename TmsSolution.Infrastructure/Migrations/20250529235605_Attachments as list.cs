using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TmsSolution.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Attachmentsaslist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachments",
                table: "TestCases");

            migrationBuilder.DropColumn(
                name: "Attachments",
                table: "Defects");

            migrationBuilder.AddColumn<Guid>(
                name: "DefectId",
                table: "Attachments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TestCaseId",
                table: "Attachments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_DefectId",
                table: "Attachments",
                column: "DefectId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_TestCaseId",
                table: "Attachments",
                column: "TestCaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Defects_DefectId",
                table: "Attachments",
                column: "DefectId",
                principalTable: "Defects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_TestCases_TestCaseId",
                table: "Attachments",
                column: "TestCaseId",
                principalTable: "TestCases",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Defects_DefectId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_TestCases_TestCaseId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_DefectId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_TestCaseId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "DefectId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "TestCaseId",
                table: "Attachments");

            migrationBuilder.AddColumn<string>(
                name: "Attachments",
                table: "TestCases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attachments",
                table: "Defects",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
