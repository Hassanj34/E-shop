namespace Catalog.Services
{
    public class ProductService(ProductDbContext dbContext)
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
