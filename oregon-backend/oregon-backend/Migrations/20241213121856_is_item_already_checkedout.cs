using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace oregon_backend.Migrations
{
    /// <inheritdoc />
    public partial class is_item_already_checkedout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCheckedOut",
                table: "Carts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCheckedOut",
                table: "Carts");
        }
    }
}
