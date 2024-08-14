using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BasketApi.Data
{
    public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<ShoppingCart> GetShoppingCartAsync(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
            if (cachedBasket == null)
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);
            var basket = await basketRepository.GetShoppingCartAsync(userName, cancellationToken);
            await cache.SetAsync(userName, JsonSerializer.SerializeToUtf8Bytes(basket), cancellationToken);
            return basket;
        }

        public async Task<bool> DeleteShoppingCartAsync(string userName, CancellationToken cancellationToken = default)
        {
            await cache.RemoveAsync(userName, cancellationToken);
            return await basketRepository.DeleteShoppingCartAsync(userName, cancellationToken);
        }

        public async Task<ShoppingCart> StoreShoppingCartAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            var result = await basketRepository.StoreShoppingCartAsync(cart, cancellationToken);
            if (result != null)
            {
                await cache.SetAsync(result.UserName, JsonSerializer.SerializeToUtf8Bytes(result), cancellationToken);
            }

            return result;
        }
    }
}
