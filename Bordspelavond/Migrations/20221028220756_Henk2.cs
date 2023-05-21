using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bordspelavond.Migrations
{
    public partial class Henk2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_BoardGame_ImageUrl",
                table: "BoardGame");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "BoardGame");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Picture",
                table: "BoardGame",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Picture",
                table: "BoardGame",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "BoardGame",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_BoardGame_ImageUrl",
                table: "BoardGame",
                column: "ImageUrl");
        }
    }
}
