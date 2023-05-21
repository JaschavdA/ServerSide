using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bordspelavond.Migrations
{
    public partial class AddeddietpreferencestoWebsiteUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastGameNightSignUp",
                table: "WebsiteUser");

            migrationBuilder.AddColumn<bool>(
                name: "ConsumesAlcohol",
                table: "WebsiteUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVegetarian",
                table: "WebsiteUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LactoseIntolerant",
                table: "WebsiteUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NutAllergy",
                table: "WebsiteUser",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsumesAlcohol",
                table: "WebsiteUser");

            migrationBuilder.DropColumn(
                name: "IsVegetarian",
                table: "WebsiteUser");

            migrationBuilder.DropColumn(
                name: "LactoseIntolerant",
                table: "WebsiteUser");

            migrationBuilder.DropColumn(
                name: "NutAllergy",
                table: "WebsiteUser");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastGameNightSignUp",
                table: "WebsiteUser",
                type: "datetime2",
                nullable: true);
        }
    }
}
