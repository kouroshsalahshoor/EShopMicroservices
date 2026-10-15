using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.Get;

public record Query(PaginationRequest PaginationRequest) : IQuery<Result>;

public record Result(PaginatedResult<OrderDto> Orders);