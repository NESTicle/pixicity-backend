using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Pixicity.Data.Migrations
{
    public partial class addtable__configuracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Configuracion",
                schema: "Web",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SiteName = table.Column<string>(type: "text", nullable: true),
                    Slogan = table.Column<string>(type: "text", nullable: true),
                    URL = table.Column<string>(type: "text", nullable: true),
                    MaintenanceMode = table.Column<bool>(type: "boolean", nullable: false),
                    MaintenanceMessage = table.Column<string>(type: "text", nullable: true),
                    OnlineUsersTime = table.Column<string>(type: "text", nullable: true),
                    HeaderScript = table.Column<string>(type: "text", nullable: true),
                    FooterScript = table.Column<string>(type: "text", nullable: true),
                    Banner300x250 = table.Column<string>(type: "text", nullable: true),
                    Banner468x60 = table.Column<string>(type: "text", nullable: true),
                    Banner160x600 = table.Column<string>(type: "text", nullable: true),
                    Banner728x90 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuracion", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Configuracion_Id",
                schema: "Web",
                table: "Configuracion",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configuracion",
                schema: "Web");
        }
    }
}
