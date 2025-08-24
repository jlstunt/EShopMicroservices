
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;


namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) 
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        //Retrieve from cache if not null
        var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket))
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;

        //if not in cache, retrieve from repository and store in cache
        var basket = await repository.GetBasket(userName, cancellationToken);
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        //Store in repository and cache
        await repository.StoreBasket(basket, cancellationToken);
        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        //Delete from repository and cache
        await repository.DeleteBasket(userName, cancellationToken);
        await cache.RemoveAsync(userName, cancellationToken);

        return true;
    }
}
