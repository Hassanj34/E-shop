using MassTransit;
using ServiceDefaults.Messaging.Events;

namespace Catalog.Services
{
    public class ProductService(ProductDbContext dbContext, IBus bus)
    {
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await dbContext.Products.FindAsync(id);
        }

        public async Task CreateProductAsync(Product product)
        {
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdatedProductAsync(Product product, Product inputProduct)
        {
            // if price has changed, raise ProductPriceChanged integration event
            if (product.Price != inputProduct.Price)
            {
                // Public product price changed integration event for update basket prices
                var integrationEvent = new ProductPriceChangedIntegrationEvent
                {
                    ProductId = product.Id,
                    Name = inputProduct.Name,
                    Price = inputProduct.Price,
                    Description = inputProduct.Description,
                    ImageUrl = inputProduct.ImageUrl,
                };

                await bus.Publish(integrationEvent);
            }

            product.Name = inputProduct.Name;
            product.Description = inputProduct.Description;
            product.Price = inputProduct.Price;
            product.ImageUrl = inputProduct.ImageUrl;

            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
        }
    }
}
