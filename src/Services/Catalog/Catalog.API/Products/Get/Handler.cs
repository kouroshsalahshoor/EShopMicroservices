namespace Catalog.API.Products.Get;

public record Query() : IQuery<Result>;
public record Result(IEnumerable<Product> Items);

internal class Handler(IDocumentSession session, ILogger<Handler> logger) : IQueryHandler<Query, Result>
{
    public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Products Get Handler called with {@query}", query);

        var items = await session.Query<Product>().ToListAsync(cancellationToken);

        return new Result(items);
    }
}
