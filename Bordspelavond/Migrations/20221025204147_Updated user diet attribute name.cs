using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bordspelavond.Migrations
{
    public partial class Updateduserdietattributename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConsumesAlcohol",
                table: "WebsiteUser",
                newName: "AlcoholFree");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AlcoholFree",
                table: "WebsiteUser",
                newName: "ConsumesAlcohol");
        }
    }
}
