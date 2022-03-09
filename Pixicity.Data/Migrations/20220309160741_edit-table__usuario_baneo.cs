using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pixicity.Data.Migrations
{
    public partial class edittable__usuario_baneo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RazonBaneo",
                schema: "Seguridad",
                table: "Usuarios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TiempoBaneado",
                schema: "Seguridad",
                table: "Usuarios",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RazonBaneo",
                schema: "Seguridad",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "TiempoBaneado",
                schema: "Seguridad",
                table: "Usuarios");
        }
    }
}
