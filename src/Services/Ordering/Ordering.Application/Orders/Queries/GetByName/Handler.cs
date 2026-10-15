namespace Ordering.Application.Orders.Queries.GetByName;

public class Handler(IApplicationDbContext dbContext) : IQueryHandler<Query, Result>
{
    public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderName.Value.Contains(query.Name))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);

        return new Result(orders.ToOrderDtoList());
    }
}