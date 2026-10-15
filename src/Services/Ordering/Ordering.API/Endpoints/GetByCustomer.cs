using Ordering.Application.Orders.Queries.GetByCustomer;

namespace Ordering.API.Endpoints;

//public record GetOrdersByCustomerRequest(Guid CustomerId);
public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);
public class GetByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (ISender sender, Guid customerId) =>
        {
            var result = await sender.Send(new Query(customerId));

            var response = result.Adapt<GetOrdersByCustomerResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrdersByCustomer")
        .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders By Customer")
        .WithDescription("Get Orders By Customer");
    }
}