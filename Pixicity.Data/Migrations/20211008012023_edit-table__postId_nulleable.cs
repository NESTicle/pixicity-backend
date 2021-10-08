using Microsoft.EntityFrameworkCore.Migrations;

namespace Pixicity.Data.Migrations
{
    public partial class edittable__postId_nulleable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitores_Posts_PostId",
                schema: "Logs",
                table: "Monitores");

            migrationBuilder.AlterColumn<long>(
                name: "PostId",
                schema: "Logs",
                table: "Monitores",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Monitores_Posts_PostId",
                schema: "Logs",
                table: "Monitores",
                column: "PostId",
                principalSchema: "Post",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monitores_Posts_PostId",
                schema: "Logs",
                table: "Monitores");

            migrationBuilder.AlterColumn<long>(
                name: "PostId",
                schema: "Logs",
                table: "Monitores",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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
    }
}
