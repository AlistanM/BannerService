using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BannerService.Migrations
{
    /// <inheritdoc />
    public partial class RefactorTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banners_Features_FeaturesId",
                table: "Banners");

            migrationBuilder.RenameColumn(
                name: "FeaturesId",
                table: "Banners",
                newName: "FeatureId");

            migrationBuilder.RenameIndex(
                name: "IX_Banners_FeaturesId",
                table: "Banners",
                newName: "IX_Banners_FeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Banners_Features_FeatureId",
                table: "Banners",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banners_Features_FeatureId",
                table: "Banners");

            migrationBuilder.RenameColumn(
                name: "FeatureId",
                table: "Banners",
                newName: "FeaturesId");

            migrationBuilder.RenameIndex(
                name: "IX_Banners_FeatureId",
                table: "Banners",
                newName: "IX_Banners_FeaturesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Banners_Features_FeaturesId",
                table: "Banners",
                column: "FeaturesId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
