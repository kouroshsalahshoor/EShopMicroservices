namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session)
    : IBasketRepository
{
    public async Task<Cart> Get(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await session.LoadAsync<Cart>(userName, cancellationToken);

        return basket ?? throw new BasketNotFoundException(userName);
        //return basket is null ? throw new BasketNotFoundException(userName) : basket;
    }

    public async Task<Cart> Save(Cart basket, CancellationToken cancellationToken = default)
    {
        session.Store(basket);
        await session.SaveChangesAsync(cancellationToken);
        return basket;
    }

    public async Task<bool> Delete(string userName, CancellationToken cancellationToken = default)
    {
        session.Delete<Cart>(userName);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }
}