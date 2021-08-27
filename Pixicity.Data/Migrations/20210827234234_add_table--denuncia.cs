using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pixicity.Data.Migrations
{
    public partial class add_tabledenuncia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Denuncias",
                schema: "Web",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PostId = table.Column<long>(type: "bigint", nullable: false),
                    UsuarioDenunciaId = table.Column<long>(type: "bigint", nullable: false),
                    RazonDenunciaId = table.Column<long>(type: "bigint", nullable: false),
                    Comentarios = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: false),
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
                    table.PrimaryKey("PK_Denuncias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Denuncias_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "Post",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Denuncias_Usuarios_UsuarioDenunciaId",
                        column: x => x.UsuarioDenunciaId,
                        principalSchema: "Seguridad",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Denuncias_Id",
                schema: "Web",
                table: "Denuncias",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Denuncias_PostId",
                schema: "Web",
                table: "Denuncias",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Denuncias_UsuarioDenunciaId",
                schema: "Web",
                table: "Denuncias",
                column: "UsuarioDenunciaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Denuncias",
                schema: "Web");
        }
    }
}
