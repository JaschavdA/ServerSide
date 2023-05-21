using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bordspelavond.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebsiteUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteUser", x => x.Id);
                    table.UniqueConstraint("AK_WebsiteUser_Email", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "BoardGame",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Is18Plus = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    TypeOfGame = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGame", x => x.Id);
                    table.UniqueConstraint("AK_BoardGame_ImageUrl", x => x.ImageUrl);
                    table.ForeignKey(
                        name: "FK_BoardGame_WebsiteUser_UserId",
                        column: x => x.UserId,
                        principalTable: "WebsiteUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameNight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizerId = table.Column<int>(type: "int", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    MaxNumberOfPlayers = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Is18Plus = table.Column<bool>(type: "bit", nullable: false),
                    VegetarianSnacks = table.Column<bool>(type: "bit", nullable: false),
                    LactoseFreeSnacks = table.Column<bool>(type: "bit", nullable: false),
                    NutFreeSnacks = table.Column<bool>(type: "bit", nullable: false),
                    AlcoholFreeDrinks = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameNight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameNight_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GameNight_WebsiteUser_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "WebsiteUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BoardGameGameNight",
                columns: table => new
                {
                    GameNightsId = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGameGameNight", x => new { x.GameNightsId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_BoardGameGameNight_BoardGame_GamesId",
                        column: x => x.GamesId,
                        principalTable: "BoardGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardGameGameNight_GameNight_GameNightsId",
                        column: x => x.GameNightsId,
                        principalTable: "GameNight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameNightWebsiteUser",
                columns: table => new
                {
                    GameNightId = table.Column<int>(type: "int", nullable: false),
                    ParticipantsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameNightWebsiteUser", x => new { x.GameNightId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_GameNightWebsiteUser_GameNight_GameNightId",
                        column: x => x.GameNightId,
                        principalTable: "GameNight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameNightWebsiteUser_WebsiteUser_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "WebsiteUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardGame_UserId",
                table: "BoardGame",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameGameNight_GamesId",
                table: "BoardGameGameNight",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_GameNight_AddressId",
                table: "GameNight",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_GameNight_OrganizerId",
                table: "GameNight",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_GameNightWebsiteUser_ParticipantsId",
                table: "GameNightWebsiteUser",
                column: "ParticipantsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardGameGameNight");

            migrationBuilder.DropTable(
                name: "GameNightWebsiteUser");

            migrationBuilder.DropTable(
                name: "BoardGame");

            migrationBuilder.DropTable(
                name: "GameNight");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "WebsiteUser");
        }
    }
}
