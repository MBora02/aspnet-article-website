using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticleWebsite.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class article_tac_releation_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleTagClouds");

            migrationBuilder.AddColumn<int>(
                name: "TagCloudId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_TagCloudId",
                table: "Articles",
                column: "TagCloudId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_TagClouds_TagCloudId",
                table: "Articles",
                column: "TagCloudId",
                principalTable: "TagClouds",
                principalColumn: "TagCloudId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_TagClouds_TagCloudId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_TagCloudId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "TagCloudId",
                table: "Articles");

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
    }
}
