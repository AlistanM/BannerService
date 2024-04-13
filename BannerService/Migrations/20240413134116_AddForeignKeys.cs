using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BannerService.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BannerTag_TagId",
                table: "BannerTag",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_BannerTag_Banners_BannerId",
                table: "BannerTag",
                column: "BannerId",
                principalTable: "Banners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BannerTag_Tags_TagId",
                table: "BannerTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BannerTag_Banners_BannerId",
                table: "BannerTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BannerTag_Tags_TagId",
                table: "BannerTag");

            migrationBuilder.DropIndex(
                name: "IX_BannerTag_TagId",
                table: "BannerTag");
        }
    }
}
