namespace Catalog.API.Products.Get;

public record Query(int? PageNumber = 1, int? PageSize = 10) : IQuery<Result>;
public record Result(IEnumerable<Product> Items);

internal class Handler(
    IDocumentSession session 
    //ILogger<Handler> logger
    ) 
    : IQueryHandler<Query, Result>
{
    public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
    {
        //logger.LogInformation("Products Get Handler called with {@query}", query);

        var items = await session
            .Query<Product>()
            .ToPagedListAsync(
                query.PageNumber ?? 1, 
                query.PageSize ?? 10, 
                cancellationToken
                );

        return new Result(items);
    }
}
