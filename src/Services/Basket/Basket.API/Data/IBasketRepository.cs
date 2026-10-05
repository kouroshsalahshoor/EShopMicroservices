namespace Basket.API.Data;

public interface IBasketRepository
{
    Task<Cart> Get(string userName, CancellationToken cancellationToken = default);
    Task<Cart> Save(Cart basket, CancellationToken cancellationToken = default);
    Task<bool> Delete(string userName, CancellationToken cancellationToken = default);
}