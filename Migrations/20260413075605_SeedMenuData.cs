using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace qrmenusistemiuygulama18.Migrations
{
    /// <inheritdoc />
    public partial class SeedMenuData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Kebaplar & Izgaralar" },
                    { 2, "Burger & Pizza" },
                    { 3, "Tatlılar" },
                    { 4, "İçecekler" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Hakiki tereyağlı, özel soslu İskender.", "https://images.unsplash.com/photo-1647413669145-2f96eecbee0f?w=600&h=400&fit=crop", "İskender Kebap", 350.00m },
                    { 2, 1, "Acılı, lavaş ve köz sebze ile servis edilir.", "https://images.unsplash.com/photo-1603903173760-496395b0b6da?w=600&h=400&fit=crop", "Adana Kebap", 280.00m },
                    { 3, 1, "Marine edilmiş parça tavuk, pilav ve salata ile.", "https://images.unsplash.com/photo-1599598425947-33002629ee8f?w=600&h=400&fit=crop", "Tavuk Şiş", 240.00m },
                    { 4, 2, "Dana eti, cheddar, marul ve patates kızartması.", "https://images.unsplash.com/photo-1568901346375-23c9450c58cd?w=600&h=400&fit=crop", "Klasik Burger🍔", 220.00m },
                    { 5, 2, "Sucuk, sosis, zeytin, mantar ve mısır.", "https://images.unsplash.com/photo-1513104890138-7c749659a591?w=600&h=400&fit=crop", "Karışık Pizza🍕", 260.00m },
                    { 6, 3, "Bol cevizli fırın sütlaç.", "https://images.unsplash.com/photo-1626002161175-1033230b0507?w=600&h=400&fit=crop", "Fırın Sütlaç", 90.00m },
                    { 7, 3, "Özel peynirli, dondurma ile servis edilir.", "https://placehold.co/600x400?text=Künefe", "Künefe", 150.00m },
                    { 8, 4, "Bol köpüklü yayık ayranı.", "https://placehold.co/600x400?text=Ayran", "Ayran", 40.00m },
                    { 9, 4, "Kutu kola 330ml.", "https://images.unsplash.com/photo-1622483767028-3f66f32aef97?w=600&h=400&fit=crop", "Coca Cola", 50.00m },
                    { 10, 4, "Lokum ile.", "https://images.unsplash.com/photo-1516086887556-9b5a8e0e7a8e?w=600&h=400&fit=crop", "Türk Kahvesi", 60.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
