using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pixicity.Data.Migrations
{
    public partial class addtable__monitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Logs");

            migrationBuilder.CreateTable(
                name: "Monitores",
                schema: "Logs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    UsuarioQueHaceAccionId = table.Column<long>(type: "bigint", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Leido = table.Column<bool>(type: "boolean", nullable: false),
                    Mensaje = table.Column<string>(type: "text", nullable: true),
                    Tipo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monitores_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "Seguridad",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Monitores_Usuarios_UsuarioQueHaceAccionId",
                        column: x => x.UsuarioQueHaceAccionId,
                        principalSchema: "Seguridad",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Monitores_Id",
                schema: "Logs",
                table: "Monitores",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Monitores_UsuarioId",
                schema: "Logs",
                table: "Monitores",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Monitores_UsuarioQueHaceAccionId",
                schema: "Logs",
                table: "Monitores",
                column: "UsuarioQueHaceAccionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Monitores",
                schema: "Logs");
        }
    }
}
