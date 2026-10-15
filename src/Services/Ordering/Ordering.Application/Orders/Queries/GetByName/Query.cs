namespace Ordering.Application.Orders.Queries.GetByName;

public record Query(string Name) : IQuery<Result>;

public record Result(IEnumerable<OrderDto> Orders);