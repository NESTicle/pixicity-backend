using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pixicity.Data.Migrations
{
    public partial class edit_tablefavoritoPost__newColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                schema: "Post",
                table: "FavoritosPost",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualiza",
                schema: "Post",
                table: "FavoritosPost",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaElimina",
                schema: "Post",
                table: "FavoritosPost",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioActualiza",
                schema: "Post",
                table: "FavoritosPost",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioElimina",
                schema: "Post",
                table: "FavoritosPost",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioRegistra",
                schema: "Post",
                table: "FavoritosPost",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eliminado",
                schema: "Post",
                table: "FavoritosPost");

            migrationBuilder.DropColumn(
                name: "FechaActualiza",
                schema: "Post",
                table: "FavoritosPost");

            migrationBuilder.DropColumn(
                name: "FechaElimina",
                schema: "Post",
                table: "FavoritosPost");

            migrationBuilder.DropColumn(
                name: "UsuarioActualiza",
                schema: "Post",
                table: "FavoritosPost");

            migrationBuilder.DropColumn(
                name: "UsuarioElimina",
                schema: "Post",
                table: "FavoritosPost");

            migrationBuilder.DropColumn(
                name: "UsuarioRegistra",
                schema: "Post",
                table: "FavoritosPost");
        }
    }
}
