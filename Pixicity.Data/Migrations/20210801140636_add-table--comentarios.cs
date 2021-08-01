using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pixicity.Data.Migrations
{
    public partial class addtablecomentarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Comentarios",
                schema: "Seguridad",
                table: "Usuarios",
                newName: "CantidadComentarios");

            migrationBuilder.RenameColumn(
                name: "Comentarios",
                schema: "Post",
                table: "Posts",
                newName: "CantidadComentarios");

            migrationBuilder.CreateTable(
                name: "Comentarios",
                schema: "Post",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PostId = table.Column<long>(type: "bigint", nullable: false),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    Contenido = table.Column<string>(type: "text", nullable: false),
                    FechaComentario = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Votos = table.Column<int>(type: "integer", nullable: false),
                    IP = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    Eliminado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentarios_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "Post",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "Seguridad",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_Id",
                schema: "Post",
                table: "Comentarios",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_PostId",
                schema: "Post",
                table: "Comentarios",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_UsuarioId",
                schema: "Post",
                table: "Comentarios",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios",
                schema: "Post");

            migrationBuilder.RenameColumn(
                name: "CantidadComentarios",
                schema: "Seguridad",
                table: "Usuarios",
                newName: "Comentarios");

            migrationBuilder.RenameColumn(
                name: "CantidadComentarios",
                schema: "Post",
                table: "Posts",
                newName: "Comentarios");
        }
    }
}
