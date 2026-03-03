using Basket.Utilities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Services
{
    public class BasketService(RedisKeyFetcher redisKeyFetcher, IDistributedCache redisCache, CatalogApiClient catalogApiClient)
    {
        public async Task<ShoppingCart?> GetBasket(string userName)
        {
            var basket = await redisCache.GetStringAsync(userName);
            return string.IsNullOrEmpty(basket) ? null : JsonSerializer.Deserialize<ShoppingCart>(basket);
        }

        public async Task UpdateBasket(ShoppingCart basket)
        {
            foreach(var item in basket.Items)
            {
                var product = await catalogApiClient.GetProductById(item.ProductId);
                item.Price = product.Price;
                item.ProductName = product.Name;
            }

            await redisCache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket));
        }

        public async Task DeleteBasket(string userName)
        {
            await redisCache.RemoveAsync(userName);
        }

        internal async Task UpdateBasketItemProductPrices(int productId, decimal price)
        { 
            foreach (var key in redisKeyFetcher.GetAllKeys()) 
            { 
                var basket = await GetBasket(key.ToString());

                var item = basket!.Items.FirstOrDefault(x => x.ProductId == productId);
                if (item != null)
                {
                    item.Price = price;
                    await redisCache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket));
                }
            }
        }
    }
}
