using MassTransit;
using ServiceDefaults.Messaging.Events;

namespace Basket.EventHandlers
{
    public class ProductPriceChangedEventHandler(BasketService basketService)
        : IConsumer<ProductPriceChangedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ProductPriceChangedIntegrationEvent> context)
        {
            await basketService.UpdateBasketItemProductPrices(context.Message.ProductId, context.Message.Price);
        }
    }
}
