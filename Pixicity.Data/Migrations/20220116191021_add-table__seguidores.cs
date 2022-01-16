using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pixicity.Data.Migrations
{
    public partial class addtable__seguidores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seguidores",
                schema: "Seguridad",
                table: "Usuarios");

            migrationBuilder.CreateTable(
                name: "UsuarioSeguidores",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SeguidoId = table.Column<long>(type: "bigint", nullable: false),
                    SeguidorId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_UsuarioSeguidores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioSeguidores_Usuarios_SeguidoId",
                        column: x => x.SeguidoId,
                        principalSchema: "Seguridad",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioSeguidores_Usuarios_SeguidorId",
                        column: x => x.SeguidorId,
                        principalSchema: "Seguridad",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioSeguidores_Id",
                schema: "Seguridad",
                table: "UsuarioSeguidores",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioSeguidores_SeguidoId",
                schema: "Seguridad",
                table: "UsuarioSeguidores",
                column: "SeguidoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioSeguidores_SeguidorId",
                schema: "Seguridad",
                table: "UsuarioSeguidores",
                column: "SeguidorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioSeguidores",
                schema: "Seguridad");

            migrationBuilder.AddColumn<int>(
                name: "Seguidores",
                schema: "Seguridad",
                table: "Usuarios",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
