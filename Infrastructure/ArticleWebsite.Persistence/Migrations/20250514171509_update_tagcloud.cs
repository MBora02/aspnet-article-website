using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticleWebsite.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class update_tagcloud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagClouds_Articles_ArticleId",
                table: "TagClouds");

            migrationBuilder.DropIndex(
                name: "IX_TagClouds_ArticleId",
                table: "TagClouds");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "TagClouds");

            migrationBuilder.CreateTable(
                name: "ArticleTagCloud",
                columns: table => new
                {
                    ArticlesArticleId = table.Column<int>(type: "int", nullable: false),
                    TagCloudsTagCloudId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTagCloud", x => new { x.ArticlesArticleId, x.TagCloudsTagCloudId });
                    table.ForeignKey(
                        name: "FK_ArticleTagCloud_Articles_ArticlesArticleId",
                        column: x => x.ArticlesArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleTagCloud_TagClouds_TagCloudsTagCloudId",
                        column: x => x.TagCloudsTagCloudId,
                        principalTable: "TagClouds",
                        principalColumn: "TagCloudId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTagCloud_TagCloudsTagCloudId",
                table: "ArticleTagCloud",
                column: "TagCloudsTagCloudId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleTagCloud");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "TagClouds",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagClouds_ArticleId",
                table: "TagClouds",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_TagClouds_Articles_ArticleId",
                table: "TagClouds",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId");
        }
    }
}
