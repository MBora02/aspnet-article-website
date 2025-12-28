using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticleWebsite.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class update_articletagcloud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleTagCloud");

            migrationBuilder.CreateTable(
                name: "ArticleTagClouds",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    TagCloudId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTagClouds", x => new { x.ArticleId, x.TagCloudId });
                    table.ForeignKey(
                        name: "FK_ArticleTagClouds_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleTagClouds_TagClouds_TagCloudId",
                        column: x => x.TagCloudId,
                        principalTable: "TagClouds",
                        principalColumn: "TagCloudId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTagClouds_TagCloudId",
                table: "ArticleTagClouds",
                column: "TagCloudId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleTagClouds");

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
    }
}
