using Microsoft.EntityFrameworkCore.Migrations;

namespace Pustok.Migrations
{
    public partial class TablesCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(maxLength: 250, nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    Text = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title1 = table.Column<string>(maxLength: 35, nullable: false),
                    Title2 = table.Column<string>(maxLength: 35, nullable: true),
                    Text = table.Column<string>(maxLength: 250, nullable: true),
                    BtnText = table.Column<string>(maxLength: 35, nullable: true),
                    BtnUrl = table.Column<string>(maxLength: 250, nullable: true),
                    Image = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Sliders");
        }
    }
}
