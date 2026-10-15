namespace Ordering.Application.Orders.Queries.GetByCustomer;

public class Handler(IApplicationDbContext dbContext) : IQueryHandler<Query, Result>
{
    public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
                        .Include(o => o.OrderItems)
                        .AsNoTracking()
                        .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
                        .OrderBy(o => o.OrderName.Value)
                        .ToListAsync(cancellationToken);

        return new Result(orders.ToOrderDtoList());
    }
}