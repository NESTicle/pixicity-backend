using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pixicity.Data.Migrations
{
    public partial class addtable__visitas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Visitas",
                schema: "Web",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: true),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_Visitas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visitas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "Seguridad",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_Id",
                schema: "Web",
                table: "Visitas",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_UsuarioId",
                schema: "Web",
                table: "Visitas",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visitas",
                schema: "Web");
        }
    }
}
