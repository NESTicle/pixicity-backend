using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pixicity.Data.Migrations
{
    public partial class addtable__historial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Historial",
                schema: "Web",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TipoId = table.Column<long>(type: "bigint", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: true),
                    Accion = table.Column<string>(type: "text", nullable: false),
                    Razon = table.Column<string>(type: "text", nullable: false),
                    IP = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
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
                    table.PrimaryKey("PK_Historial", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Historial_Id",
                schema: "Web",
                table: "Historial",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historial",
                schema: "Web");
        }
    }
}
