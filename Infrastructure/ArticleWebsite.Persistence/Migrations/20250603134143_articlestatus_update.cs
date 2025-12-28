using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticleWebsite.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class articlestatus_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ArticleStatuses",
                keyColumn: "ArticleStatusId",
                keyValue: 1,
                column: "Name",
                value: "Saved");

            migrationBuilder.UpdateData(
                table: "ArticleStatuses",
                keyColumn: "ArticleStatusId",
                keyValue: 2,
                column: "Name",
                value: "Pending");

            migrationBuilder.UpdateData(
                table: "ArticleStatuses",
                keyColumn: "ArticleStatusId",
                keyValue: 3,
                column: "Name",
                value: "Approved");

            migrationBuilder.InsertData(
                table: "ArticleStatuses",
                columns: new[] { "ArticleStatusId", "Name" },
                values: new object[] { 4, "Rejected" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ArticleStatuses",
                keyColumn: "ArticleStatusId",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "ArticleStatuses",
                keyColumn: "ArticleStatusId",
                keyValue: 1,
                column: "Name",
                value: "Pending");

            migrationBuilder.UpdateData(
                table: "ArticleStatuses",
                keyColumn: "ArticleStatusId",
                keyValue: 2,
                column: "Name",
                value: "Approved");

            migrationBuilder.UpdateData(
                table: "ArticleStatuses",
                keyColumn: "ArticleStatusId",
                keyValue: 3,
                column: "Name",
                value: "Rejected");
        }
    }
}
