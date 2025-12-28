using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticleWebsite.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class tag_cloud_update_mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagClouds_Articles_ArticleId",
                table: "TagClouds");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "TagClouds",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TagClouds_Articles_ArticleId",
                table: "TagClouds",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagClouds_Articles_ArticleId",
                table: "TagClouds");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "TagClouds",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TagClouds_Articles_ArticleId",
                table: "TagClouds",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
