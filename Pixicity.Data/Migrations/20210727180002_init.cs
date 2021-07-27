using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pixicity.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Parametros");

            migrationBuilder.EnsureSchema(
                name: "Seguridad");

            migrationBuilder.CreateTable(
                name: "Paises",
                schema: "Parametros",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ISO2 = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    ISO3 = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
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
                    table.PrimaryKey("PK_Paises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                schema: "Parametros",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPais = table.Column<long>(type: "bigint", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_Estados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estados_Paises_IdPais",
                        column: x => x.IdPais,
                        principalSchema: "Parametros",
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EstadoId = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false),
                    Password = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FechaNacimiento = table.Column<string>(type: "text", nullable: true),
                    Genero = table.Column<string>(type: "text", nullable: true),
                    Rango = table.Column<int>(type: "integer", nullable: false),
                    Puntos = table.Column<int>(type: "integer", nullable: false),
                    Comentarios = table.Column<int>(type: "integer", nullable: false),
                    Seguidores = table.Column<int>(type: "integer", nullable: false),
                    UltimaConexion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UltimaIP = table.Column<string>(type: "text", nullable: true),
                    Baneado = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalSchema: "Parametros",
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estados_Id",
                schema: "Parametros",
                table: "Estados",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estados_IdPais",
                schema: "Parametros",
                table: "Estados",
                column: "IdPais");

            migrationBuilder.CreateIndex(
                name: "IX_Paises_Id",
                schema: "Parametros",
                table: "Paises",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EstadoId",
                schema: "Seguridad",
                table: "Usuarios",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Id",
                schema: "Seguridad",
                table: "Usuarios",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Estados",
                schema: "Parametros");

            migrationBuilder.DropTable(
                name: "Paises",
                schema: "Parametros");
        }
    }
}
