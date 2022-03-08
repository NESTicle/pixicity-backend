using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pixicity.Data.Migrations
{
    public partial class edittable__configuracion__record : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RecordOnlineTime",
                schema: "Web",
                table: "Configuracion",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RecordOnlineUsers",
                schema: "Web",
                table: "Configuracion",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordOnlineTime",
                schema: "Web",
                table: "Configuracion");

            migrationBuilder.DropColumn(
                name: "RecordOnlineUsers",
                schema: "Web",
                table: "Configuracion");
        }
    }
}
