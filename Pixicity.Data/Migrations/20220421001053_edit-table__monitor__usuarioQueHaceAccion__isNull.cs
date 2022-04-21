using Microsoft.EntityFrameworkCore.Migrations;

namespace Pixicity.Data.Migrations
{
    public partial class edittable__monitor__usuarioQueHaceAccion__isNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitores_Usuarios_UsuarioQueHaceAccionId",
                schema: "Logs",
                table: "Monitores");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioQueHaceAccionId",
                schema: "Logs",
                table: "Monitores",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitores_Usuarios_UsuarioQueHaceAccionId",
                schema: "Logs",
                table: "Monitores",
                column: "UsuarioQueHaceAccionId",
                principalSchema: "Seguridad",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitores_Usuarios_UsuarioQueHaceAccionId",
                schema: "Logs",
                table: "Monitores");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioQueHaceAccionId",
                schema: "Logs",
                table: "Monitores",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Monitores_Usuarios_UsuarioQueHaceAccionId",
                schema: "Logs",
                table: "Monitores",
                column: "UsuarioQueHaceAccionId",
                principalSchema: "Seguridad",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
