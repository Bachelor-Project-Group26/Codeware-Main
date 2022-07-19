using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPR_API.Migrations
{
    public partial class UpdatedUserDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "UserDetails",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "UserDetails",
                type: "TEXT",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "UserDetails",
                type: "TEXT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "UserDetails",
                type: "TEXT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "UserDetails",
                type: "BLOB",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserPasswords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Hash = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Salt = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPasswords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPasswords");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "UserDetails");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "UserDetails",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Hash = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Salt = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }
    }
}
