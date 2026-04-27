using Microsoft.EntityFrameworkCore;
using qrmenusistemiuygulama18.Models;

namespace qrmenusistemiuygulama18.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed 20 Tables
            for (int i = 1; i <= 20; i++)
            {
                modelBuilder.Entity<Table>().HasData(new Table { Id = i, TableNumber = i });
            }

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Kebaplar & Izgaralar" },
                new Category { Id = 2, Name = "Burger & Pizza" },
                new Category { Id = 3, Name = "Tatlılar" },
                new Category { Id = 4, Name = "İçecekler" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, CategoryId = 1, Name = "İskender Kebap", Description = "Hakiki tereyağlı, özel soslu İskender.", Price = 350.00m, ImageUrl = "/images/iskender_kebab.png" },
                new Product { Id = 2, CategoryId = 1, Name = "Adana Kebap", Description = "Acılı, lavaş ve köz sebze ile servis edilir.", Price = 280.00m, ImageUrl = "/images/adana_kebap.png" },
                new Product { Id = 3, CategoryId = 1, Name = "Tavuk Şiş", Description = "Marine edilmiş parça tavuk, pilav ve salata ile.", Price = 240.00m, ImageUrl = "/images/tavuk_sis.png" },
                
                new Product { Id = 4, CategoryId = 2, Name = "Klasik Burger🍔", Description = "Dana eti, cheddar, marul ve patates kızartması.", Price = 220.00m, ImageUrl = "/images/klasik_burger.png" },
                new Product { Id = 5, CategoryId = 2, Name = "Karışık Pizza🍕", Description = "Sucuk, sosis, zeytin, mantar ve mısır.", Price = 260.00m, ImageUrl = "/images/pizza.png" },
                
                new Product { Id = 6, CategoryId = 3, Name = "Fırın Sütlaç", Description = "Bol cevizli fırın sütlaç.", Price = 90.00m, ImageUrl = "/images/firin_sutlac.png" },
                new Product { Id = 7, CategoryId = 3, Name = "Künefe", Description = "Özel peynirli, dondurma ile servis edilir.", Price = 150.00m, ImageUrl = "/images/kunefe.png" },
                
                new Product { Id = 8, CategoryId = 4, Name = "Ayran", Description = "Bol köpüklü yayık ayranı.", Price = 40.00m, ImageUrl = "/images/ayran.png" },
                new Product { Id = 9, CategoryId = 4, Name = "Coca Cola", Description = "Kutu kola 330ml.", Price = 50.00m, ImageUrl = "/images/kola.jfif" },
                new Product { Id = 10, CategoryId = 4, Name = "Türk Kahvesi", Description = "Lokum ile.", Price = 60.00m, ImageUrl = "/images/turk_kahvesi.png" }
            );

            // Relationship configuration (Optional but good practice)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);
        }
    }
}
