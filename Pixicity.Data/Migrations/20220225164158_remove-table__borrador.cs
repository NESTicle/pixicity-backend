using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pixicity.Data.Migrations
{
    public partial class removetable__borrador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Borradores",
                schema: "Post");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Borradores",
                schema: "Post",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoriaId = table.Column<long>(type: "bigint", nullable: true),
                    Contenido = table.Column<string>(type: "text", nullable: false),
                    Eliminado = table.Column<bool>(type: "boolean", nullable: false),
                    EsPrivado = table.Column<bool>(type: "boolean", nullable: false),
                    Etiquetas = table.Column<string>(type: "text", nullable: false),
                    FechaActualiza = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FechaElimina = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Smileys = table.Column<bool>(type: "boolean", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UsuarioActualiza = table.Column<string>(type: "text", nullable: true),
                    UsuarioElimina = table.Column<string>(type: "text", nullable: true),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: true),
                    UsuarioRegistra = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borradores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Borradores_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalSchema: "Parametros",
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Borradores_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "Seguridad",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Borradores_CategoriaId",
                schema: "Post",
                table: "Borradores",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Borradores_Id",
                schema: "Post",
                table: "Borradores",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Borradores_UsuarioId",
                schema: "Post",
                table: "Borradores",
                column: "UsuarioId");
        }
    }
}
