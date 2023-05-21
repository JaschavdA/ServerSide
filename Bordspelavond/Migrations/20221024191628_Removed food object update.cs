using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bordspelavond.Migrations
{
    public partial class Removedfoodobjectupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeAdded",
                table: "Food");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimeAdded",
                table: "Food",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
