namespace Catalog.API.Products.GetByCategory;

public record Query(string Category) : IQuery<Result>;
public record Result(IEnumerable<Product> Items);

internal class Handler(
    IDocumentSession session 
    //ILogger<Handler> logger
    ) 
    : IQueryHandler<Query, Result>
{
    public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
    {
        //logger.LogInformation("Products GetByCategory Handler called with {@query}", query);

        var items = await session.Query<Product>()
            .Where(x=> x.Category.Any(c => c.ToLowerInvariant().Contains(query.Category.ToLowerInvariant())))
            .ToListAsync(cancellationToken);

        return new Result(items);
    }
}
