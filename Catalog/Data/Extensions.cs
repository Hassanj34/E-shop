namespace Catalog.Data
{
    public static class Extensions
    {
        public static void UseMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
            dbContext.Database.Migrate();
            DataSeeder.Seed(dbContext);
        }
    }

    public class DataSeeder
    {
        public static void Seed(ProductDbContext dbContext)
        {
            if (dbContext.Products.Any())
                return;

            dbContext.Products.AddRange(Products);
            dbContext.SaveChanges();
        }

        public static IEnumerable<Product> Products => new List<Product>
        {
            new() {
                Name = "Laptop",
                Description = "A high-performance laptop for work and play.",
                Price = 999.99m,
                ImageUrl = "product1.png"
            },
            new() {
                Name = "Smartphone",
                Description = "A sleek smartphone with the latest features.",
                Price = 699.99m,
                ImageUrl = "product2.png"
            },
            new() {
                Name = "Headphones",
                Description = "Noise-cancelling headphones for immersive sound.",
                Price = 199.99m,
                ImageUrl = "product3.png"
            },
            new() {
                Name = "Smartwatch",
                Description = "A stylish smartwatch to keep you connected.",
                Price = 299.99m,
                ImageUrl = "product4.png"
            },
            new() {
                Name = "Tablet",
                Description = "A versatile tablet for work and entertainment.",
                Price = 499.99m,
                ImageUrl = "product5.png"
            },
            new() {
                Name = "Gaming Console",
                Description = "A powerful gaming console for endless fun.",
                Price = 399.99m,
                ImageUrl = "product6.png"
            },
            new() {
                Name = "Camera",
                Description = "A high-resolution camera for capturing memories.",
                Price = 599.99m,
                ImageUrl = "product7.png"
            },
            new() {
                Name = "Bluetooth Speaker",
                Description = "A portable Bluetooth speaker with great sound.",
                Price = 149.99m,
                ImageUrl = "product8.png"
            },
            new() {
                Name = "External Hard Drive",
                Description = "A large-capacity external hard drive for storage.",
                Price = 89.99m,
                ImageUrl = "product9.png"
            },
        };
    }
}
