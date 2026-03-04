using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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
            {
                return;
            }

            dbContext.Products.AddRange(Products);
            dbContext.SaveChanges();
        }

        public static IEnumerable<Product> Products => new List<Product>
{
    new()
    {
        Name = "Laptop",
        Description = "A high-performance laptop for work and play.",
        Price = 999.99m,
        ImageUrl = "https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=600"
    },
    new()
    {
        Name = "Smartphone",
        Description = "A sleek smartphone with the latest features.",
        Price = 699.99m,
        ImageUrl = "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=600"
    },
    new()
    {
        Name = "Headphones",
        Description = "Noise-cancelling headphones for immersive sound.",
        Price = 199.99m,
        ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=600"
    },
    new()
    {
        Name = "Smartwatch",
        Description = "A stylish smartwatch to keep you connected.",
        Price = 299.99m,
        ImageUrl = "https://images.unsplash.com/photo-1654008336992-67bfabd11916?q=80&w=764&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
    },
    new()
    {
        Name = "Tablet",
        Description = "A versatile tablet for work and entertainment.",
        Price = 499.99m,
        ImageUrl = "https://images.unsplash.com/photo-1544244015-0df4b3ffc6b0?w=600"
    },
    new()
    {
        Name = "Gaming Console",
        Description = "A powerful gaming console for endless fun.",
        Price = 399.99m,
        ImageUrl = "https://images.unsplash.com/photo-1606813907291-d86efa9b94db?w=600"
    },
    new()
    {
        Name = "Camera",
        Description = "A high-resolution camera for capturing memories.",
        Price = 599.99m,
        ImageUrl = "https://plus.unsplash.com/premium_photo-1703150072647-a888b485a107?q=80&w=1632&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
    },
    new()
    {
        Name = "Bluetooth Speaker",
        Description = "A portable Bluetooth speaker with great sound.",
        Price = 149.99m,
        ImageUrl = "https://images.unsplash.com/photo-1623169274520-36235a547737?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
    },
    new()
    {
        Name = "External Hard Drive",
        Description = "A large-capacity external hard drive for storage.",
        Price = 89.99m,
        ImageUrl = "https://images.unsplash.com/photo-1602493054445-4a0b4fa7fdd6?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NHx8ZnJlZSUyMGltYWdlcyUyMGhhcmQlMjBkcml2ZXxlbnwwfHwwfHx8MA%3D%3D"
    }
};
    }
}
