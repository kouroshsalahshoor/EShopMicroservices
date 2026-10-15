namespace Ordering.Application.Orders.Queries.GetByCustomer;

public record Query(Guid CustomerId) : IQuery<Result>;

public record Result(IEnumerable<OrderDto> Orders);