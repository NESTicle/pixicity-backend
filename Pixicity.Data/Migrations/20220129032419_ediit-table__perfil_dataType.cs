using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pixicity.Data.Migrations
{
    public partial class ediittable__perfil_dataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioPerfiles",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    CompleteName = table.Column<string>(type: "text", nullable: true),
                    PersonalMessage = table.Column<string>(type: "text", nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true),
                    Facebook = table.Column<string>(type: "text", nullable: true),
                    Twitter = table.Column<string>(type: "text", nullable: true),
                    Tiktok = table.Column<string>(type: "text", nullable: true),
                    Youtube = table.Column<string>(type: "text", nullable: true),
                    Like1 = table.Column<bool>(type: "boolean", nullable: false),
                    Like2 = table.Column<bool>(type: "boolean", nullable: false),
                    Like3 = table.Column<bool>(type: "boolean", nullable: false),
                    Like4 = table.Column<bool>(type: "boolean", nullable: false),
                    Like_All = table.Column<bool>(type: "boolean", nullable: false),
                    EstadoCivil = table.Column<string>(type: "text", nullable: true),
                    Hijos = table.Column<string>(type: "text", nullable: true),
                    VivoCon = table.Column<string>(type: "text", nullable: true),
                    Altura = table.Column<string>(type: "text", nullable: true),
                    Peso = table.Column<string>(type: "text", nullable: true),
                    ColorCabello = table.Column<string>(type: "text", nullable: true),
                    ColorOjos = table.Column<string>(type: "text", nullable: true),
                    Complexion = table.Column<string>(type: "text", nullable: true),
                    Dieta = table.Column<string>(type: "text", nullable: true),
                    Tatuajes = table.Column<bool>(type: "boolean", nullable: false),
                    Piercings = table.Column<bool>(type: "boolean", nullable: false),
                    Fumo = table.Column<string>(type: "text", nullable: true),
                    Alcohol = table.Column<string>(type: "text", nullable: true),
                    Estudios = table.Column<string>(type: "text", nullable: true),
                    Profesion = table.Column<string>(type: "text", nullable: true),
                    Empresa = table.Column<string>(type: "text", nullable: true),
                    Sector = table.Column<string>(type: "text", nullable: true),
                    InteresesProfesionales = table.Column<string>(type: "text", nullable: true),
                    HabilidadesProfesionales = table.Column<string>(type: "text", nullable: true),
                    MisIntereses = table.Column<string>(type: "text", nullable: true),
                    Hobbies = table.Column<string>(type: "text", nullable: true),
                    SeriesTV = table.Column<string>(type: "text", nullable: true),
                    MusicaFavorita = table.Column<string>(type: "text", nullable: true),
                    DeportesFavoritos = table.Column<string>(type: "text", nullable: true),
                    LibrosFavoritos = table.Column<string>(type: "text", nullable: true),
                    PeliculasFavoritas = table.Column<string>(type: "text", nullable: true),
                    ComidaFavorita = table.Column<string>(type: "text", nullable: true),
                    MisHeroesSon = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_UsuarioPerfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioPerfiles_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "Seguridad",
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPerfiles_Id",
                schema: "Seguridad",
                table: "UsuarioPerfiles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPerfiles_UsuarioId",
                schema: "Seguridad",
                table: "UsuarioPerfiles",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioPerfiles",
                schema: "Seguridad");
        }
    }
}
