using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qrmenusistemiuygulama18.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIskenderImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/images/iskender_kebab.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1647413669145-2f96eecbee0f?w=600&h=400&fit=crop");
        }
    }
}
