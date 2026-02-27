namespace Catalog.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/products");

            //Get all products
            group.MapGet("/", async (ProductService productService) =>
            {
                var products = await productService.GetProductsAsync();
                return Results.Ok(products);
            })
            .WithName("GetProducts")
            .Produces<List<Product>>(StatusCodes.Status200OK);


            //Get product by id
            group.MapGet("/{id:int}", async (int id, ProductService productService) =>
            {
                var product = await productService.GetProductByIdAsync(id);
                return product is not null ? Results.Ok(product) : Results.NotFound();
            })
            .WithName("GetProductById")
            .Produces<Product>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);


            //Create a new product
            group.MapPost("/", async (Product product, ProductService productService) =>
            {
                await productService.CreateProductAsync(product);
                return Results.Created($"/products/{product.Id}", product);
            })
            .WithName("CreateProduct")
            .Produces<Product>(StatusCodes.Status201Created);


            //Update an existing product
            group.MapPut("/{id}", async (int id, Product updatedProduct, ProductService productService) =>
            {
                var product = await productService.GetProductByIdAsync(id);
                if (product is null) return Results.NotFound();

                await productService.UpdatedProductAsync(product, updatedProduct);
                return Results.NoContent();
            })
            .WithName("UpdateProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);


            //Delete a product
            group.MapDelete("/{id}", async (int id, ProductService productService) =>
            {
                var product = await productService.GetProductByIdAsync(id);
                if (product is null) return Results.NotFound();

                await productService.DeleteProductAsync(product);
                return Results.NoContent();
            })
            .WithName("DeleteProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}
