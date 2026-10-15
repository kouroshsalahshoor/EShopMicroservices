using Ordering.Application.Orders.Commands.Delete;

namespace Ordering.API.Endpoints;

//public record DeleteOrderRequest(Guid Id);
public record DeleteOrderResponse(bool IsSuccess);
public class Delete : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (ISender sender, Guid Id) =>
        {
            var result = await sender.Send(new Command(Id));

            var response = result.Adapt<DeleteOrderResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteOrder")
        .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Order")
        .WithDescription("Delete Order");
    }
}
