using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pustok.Migrations
{
    public partial class BirthDateChangesInAuthors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirtDate",
                table: "Authors");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Authors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Authors");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirtDate",
                table: "Authors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
