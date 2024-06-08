using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Procoding.ApplicationTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedAdditionalPropertiesToJobApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "JobApplications",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobAdLink_Value",
                table: "JobApplications",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobPositionTitle",
                table: "JobApplications",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobType_Value",
                table: "JobApplications",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkLocationType_Value",
                table: "JobApplications",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "JobAdLink_Value",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "JobPositionTitle",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "JobType_Value",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "WorkLocationType_Value",
                table: "JobApplications");
        }
    }
}
