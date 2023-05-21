using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bordspelavond.Migrations
{
    public partial class AddedDateTimeforlastgamenightsignintoWebsiteUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastGameNightSignUp",
                table: "WebsiteUser",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastGameNightSignUp",
                table: "WebsiteUser");
        }
    }
}
