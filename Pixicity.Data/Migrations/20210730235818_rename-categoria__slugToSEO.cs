using Microsoft.EntityFrameworkCore.Migrations;

namespace Pixicity.Data.Migrations
{
    public partial class renamecategoria__slugToSEO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Slug",
                schema: "Parametros",
                table: "Categorias",
                newName: "SEO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SEO",
                schema: "Parametros",
                table: "Categorias",
                newName: "Slug");
        }
    }
}
