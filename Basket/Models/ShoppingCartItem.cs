namespace Basket.Models
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; } = default!;
        public string Color { get; set; } = default!;
        public int ProductId { get; set; } = default!;


        // These properties will come from the Catalog service
        public string ProductName { get; set; } = default!;
        public decimal Price { get; set; } = default!;
    }
}
