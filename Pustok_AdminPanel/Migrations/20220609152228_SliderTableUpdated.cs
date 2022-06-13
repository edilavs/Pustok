using Microsoft.EntityFrameworkCore.Migrations;

namespace Pustok.Migrations
{
    public partial class SliderTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title1",
                table: "Sliders",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(35)",
                oldMaxLength: 35);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Sliders",
                maxLength: 35,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Sliders");

            migrationBuilder.AlterColumn<string>(
                name: "Title1",
                table: "Sliders",
                type: "nvarchar(35)",
                maxLength: 35,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
