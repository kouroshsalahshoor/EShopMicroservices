using Ordering.Application.Orders.Commands.Update;

namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto Order);
public record UpdateOrderResponse(bool IsSuccess);
public class Update : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (ISender sender, UpdateOrderRequest request) =>
        {
            var command = request.Adapt<Command>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateOrderResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateOrder")
        .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Order")
        .WithDescription("Update Order");
    }
}