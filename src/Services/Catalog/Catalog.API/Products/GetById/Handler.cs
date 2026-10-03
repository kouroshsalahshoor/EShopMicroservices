
namespace Catalog.API.Products.GetById;

public record Query(Guid Id) : IQuery<Result>;
public record Result(Product Item);

internal class Handler(IDocumentSession session, ILogger<Handler> logger) : IQueryHandler<Query, Result>
{
    public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Products GetById Handler called with {@query}", query);

        var item = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (item is null)
            throw new ProductNotFoundException(query.Id);

        return new Result(item);
    }
}
