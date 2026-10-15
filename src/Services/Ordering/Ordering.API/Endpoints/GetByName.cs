using Ordering.Application.Orders.Queries.GetByName;

namespace Ordering.API.Endpoints;

//public record GetOrdersByNameRequest(string Name);
public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);
public class GetByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}", async (ISender sender, string orderName) =>
        {
            var result = await sender.Send(new Query(orderName));

            var response = result.Adapt<GetOrdersByNameResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrdersByName")
        .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders By Name")
        .WithDescription("Get Orders By Name");
    }
}