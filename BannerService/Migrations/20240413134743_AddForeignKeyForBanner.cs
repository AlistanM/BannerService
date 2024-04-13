using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BannerService.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyForBanner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Banners_FeaturesId",
                table: "Banners",
                column: "FeaturesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Banners_Features_FeaturesId",
                table: "Banners",
                column: "FeaturesId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banners_Features_FeaturesId",
                table: "Banners");

            migrationBuilder.DropIndex(
                name: "IX_Banners_FeaturesId",
                table: "Banners");
        }
    }
}
