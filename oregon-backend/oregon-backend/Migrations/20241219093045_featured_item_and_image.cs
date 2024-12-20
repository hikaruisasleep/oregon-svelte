﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace oregon_backend.Migrations
{
    /// <inheritdoc />
    public partial class featured_item_and_image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeaturedImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FeaturedItem",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeaturedImageUrl",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FeaturedItem",
                table: "Products");
        }
    }
}
