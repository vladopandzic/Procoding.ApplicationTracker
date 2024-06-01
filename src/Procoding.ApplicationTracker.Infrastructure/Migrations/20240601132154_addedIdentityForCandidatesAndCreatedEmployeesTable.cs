using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Procoding.ApplicationTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedIdentityForCandidatesAndCreatedEmployeesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Candidates",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Candidates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Candidates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Candidates",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Candidates",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Candidates",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Candidates",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Candidates",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Candidates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Candidates",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Candidates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Candidates",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Email_Value = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Candidates");
        }
    }
}
