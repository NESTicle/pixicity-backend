using Microsoft.EntityFrameworkCore.Migrations;

namespace Pixicity.Data.Migrations
{
    public partial class edittable__monitor__relationshipWith_post : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PostId",
                schema: "Logs",
                table: "Monitores",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Monitores_PostId",
                schema: "Logs",
                table: "Monitores",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitores_Posts_PostId",
                schema: "Logs",
                table: "Monitores",
                column: "PostId",
                principalSchema: "Post",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitores_Posts_PostId",
                schema: "Logs",
                table: "Monitores");

            migrationBuilder.DropIndex(
                name: "IX_Monitores_PostId",
                schema: "Logs",
                table: "Monitores");

            migrationBuilder.DropColumn(
                name: "PostId",
                schema: "Logs",
                table: "Monitores");
        }
    }
}
