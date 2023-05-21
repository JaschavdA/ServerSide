using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bordspelavond.Migrations
{
    public partial class AddedFoodobject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVegetarian = table.Column<bool>(type: "bit", nullable: false),
                    IsLactoseFree = table.Column<bool>(type: "bit", nullable: false),
                    IsNutFree = table.Column<bool>(type: "bit", nullable: false),
                    IsAlcoholFree = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GameNightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Food_GameNight_GameNightId",
                        column: x => x.GameNightId,
                        principalTable: "GameNight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Food_WebsiteUser_UserId",
                        column: x => x.UserId,
                        principalTable: "WebsiteUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Food_GameNightId",
                table: "Food",
                column: "GameNightId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_UserId",
                table: "Food",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Food");
        }
    }
}
