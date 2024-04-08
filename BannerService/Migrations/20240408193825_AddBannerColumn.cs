using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BannerService.Migrations
{
    /// <inheritdoc />
    public partial class AddBannerColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "CreatedAt",
                table: "Banners",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "UpdatedAt",
                table: "Banners",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Banners");
        }
    }
}
