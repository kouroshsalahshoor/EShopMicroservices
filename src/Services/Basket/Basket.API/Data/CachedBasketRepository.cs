using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache)
    : IBasketRepository
{
    public async Task<Cart> Get(string userName, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket))
            return JsonSerializer.Deserialize<Cart>(cachedBasket)!;

        var basket = await repository.Get(userName, cancellationToken);
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<Cart> Save(Cart basket, CancellationToken cancellationToken = default)
    {
        await repository.Save(basket, cancellationToken);

        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }

    public async Task<bool> Delete(string userName, CancellationToken cancellationToken = default)
    {
        await repository.Delete(userName, cancellationToken);

        await cache.RemoveAsync(userName, cancellationToken);

        return true;
    }
}