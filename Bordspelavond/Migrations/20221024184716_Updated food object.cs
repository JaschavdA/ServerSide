using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bordspelavond.Migrations
{
    public partial class Updatedfoodobject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAddedToGameNight",
                table: "Food",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeAdded",
                table: "Food",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAddedToGameNight",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "TimeAdded",
                table: "Food");
        }
    }
}
