namespace Basket.Endpoints
{
    public static class BasketEndpoints
    {
        public static void MapBasketEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("basket");

            group.MapGet("/{userName}", async (string userName, BasketService basketService) =>
            {
                var shoppingCart = await basketService.GetBasket(userName);
                return shoppingCart is not null ? Results.Ok(shoppingCart) : Results.NotFound();
            })
            .WithName("GetBasket")
            .Produces<ShoppingCart>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            group.MapPost("/", async (ShoppingCart shoppingCart, BasketService basketService) =>
            {
                await basketService.UpdateBasket(shoppingCart);
                return Results.Created("GetBasket", shoppingCart);
            })
            .WithName("UpdateBasket")
            .Produces<ShoppingCart>(StatusCodes.Status201Created);

            group.MapDelete("/{userName}", async (string userName, BasketService basketService) =>
            {
                await basketService.DeleteBasket(userName);
                return Results.NoContent();
            })
            .WithName("DeleteBasket")
            .Produces(StatusCodes.Status204NoContent);
        }   
    }
}
