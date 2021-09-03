using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pixicity.Data.Migrations
{
    public partial class add_tablefavoritoPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavoritosPost",
                schema: "Post",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    PostId = table.Column<long>(type: "bigint", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoritosPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoritosPost_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "Post",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoritosPost_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "Seguridad",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoritosPost_Id",
                schema: "Post",
                table: "FavoritosPost",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoritosPost_PostId",
                schema: "Post",
                table: "FavoritosPost",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritosPost_UsuarioId",
                schema: "Post",
                table: "FavoritosPost",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoritosPost",
                schema: "Post");
        }
    }
}
