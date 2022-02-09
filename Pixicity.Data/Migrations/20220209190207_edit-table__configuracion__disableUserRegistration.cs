using Microsoft.EntityFrameworkCore.Migrations;

namespace Pixicity.Data.Migrations
{
    public partial class edittable__configuracion__disableUserRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DisableUserRegistration",
                schema: "Web",
                table: "Configuracion",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DisableUserRegistrationMessage",
                schema: "Web",
                table: "Configuracion",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisableUserRegistration",
                schema: "Web",
                table: "Configuracion");

            migrationBuilder.DropColumn(
                name: "DisableUserRegistrationMessage",
                schema: "Web",
                table: "Configuracion");
        }
    }
}
