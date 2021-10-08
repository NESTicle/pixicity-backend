using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pixicity.Data.Migrations
{
    public partial class edit_tablemonitor__newColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                schema: "Logs",
                table: "Monitores",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaElimina",
                schema: "Logs",
                table: "Monitores",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioElimina",
                schema: "Logs",
                table: "Monitores",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eliminado",
                schema: "Logs",
                table: "Monitores");

            migrationBuilder.DropColumn(
                name: "FechaElimina",
                schema: "Logs",
                table: "Monitores");

            migrationBuilder.DropColumn(
                name: "UsuarioElimina",
                schema: "Logs",
                table: "Monitores");
        }
    }
}
