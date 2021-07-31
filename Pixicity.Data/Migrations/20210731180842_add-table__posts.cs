using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pixicity.Data.Migrations
{
    public partial class addtable__posts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Post");

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "Post",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoriaId = table.Column<long>(type: "bigint", nullable: false),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Contenido = table.Column<string>(type: "text", nullable: false),
                    Etiquetas = table.Column<string>(type: "text", nullable: false),
                    Puntos = table.Column<int>(type: "integer", nullable: false),
                    Comentarios = table.Column<int>(type: "integer", nullable: false),
                    Favoritos = table.Column<int>(type: "integer", nullable: false),
                    Visitantes = table.Column<int>(type: "integer", nullable: false),
                    IP = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    EsPrivado = table.Column<bool>(type: "boolean", nullable: false),
                    Sticky = table.Column<bool>(type: "boolean", nullable: false),
                    Smileys = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalSchema: "Parametros",
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "Seguridad",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoriaId",
                schema: "Post",
                table: "Posts",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Id",
                schema: "Post",
                table: "Posts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UsuarioId",
                schema: "Post",
                table: "Posts",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts",
                schema: "Post");
        }
    }
}
