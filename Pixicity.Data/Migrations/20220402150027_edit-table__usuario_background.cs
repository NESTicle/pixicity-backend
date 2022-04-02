using Microsoft.EntityFrameworkCore.Migrations;

namespace Pixicity.Data.Migrations
{
    public partial class edittable__usuario_background : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileBackground",
                schema: "Seguridad",
                table: "Usuarios",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileBackground",
                schema: "Seguridad",
                table: "Usuarios");
        }
    }
}
