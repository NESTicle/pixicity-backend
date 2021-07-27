using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pixicity.Data.Migrations
{
    public partial class addtable__rango : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rango",
                schema: "Seguridad",
                table: "Usuarios");

            migrationBuilder.AddColumn<long>(
                name: "RangoId",
                schema: "Seguridad",
                table: "Usuarios",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Rangos",
                schema: "Parametros",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Icono = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: true),
                    Eliminado = table.Column<bool>(type: "boolean", nullable: false),
                    UsuarioRegistra = table.Column<string>(type: "text", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UsuarioActualiza = table.Column<string>(type: "text", nullable: true),
                    FechaActualiza = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UsuarioElimina = table.Column<string>(type: "text", nullable: true),
                    FechaElimina = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rangos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RangoId",
                schema: "Seguridad",
                table: "Usuarios",
                column: "RangoId");

            migrationBuilder.CreateIndex(
                name: "IX_Rangos_Id",
                schema: "Parametros",
                table: "Rangos",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Rangos_RangoId",
                schema: "Seguridad",
                table: "Usuarios",
                column: "RangoId",
                principalSchema: "Parametros",
                principalTable: "Rangos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Rangos_RangoId",
                schema: "Seguridad",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Rangos",
                schema: "Parametros");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_RangoId",
                schema: "Seguridad",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "RangoId",
                schema: "Seguridad",
                table: "Usuarios");

            migrationBuilder.AddColumn<int>(
                name: "Rango",
                schema: "Seguridad",
                table: "Usuarios",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
