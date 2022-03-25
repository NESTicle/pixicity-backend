using Microsoft.EntityFrameworkCore.Migrations;

namespace Pixicity.Data.Migrations
{
    public partial class edittable__sessions_IP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Usuarios_UsuarioId",
                schema: "Seguridad",
                table: "Sessions");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioId",
                schema: "Seguridad",
                table: "Sessions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "IP",
                schema: "Seguridad",
                table: "Sessions",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Usuarios_UsuarioId",
                schema: "Seguridad",
                table: "Sessions",
                column: "UsuarioId",
                principalSchema: "Seguridad",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Usuarios_UsuarioId",
                schema: "Seguridad",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "IP",
                schema: "Seguridad",
                table: "Sessions");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioId",
                schema: "Seguridad",
                table: "Sessions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Usuarios_UsuarioId",
                schema: "Seguridad",
                table: "Sessions",
                column: "UsuarioId",
                principalSchema: "Seguridad",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
