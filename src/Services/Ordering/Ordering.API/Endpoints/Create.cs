using Ordering.Application.Orders.Commands.Create;

namespace Ordering.API.Endpoints;

public record CreateOrderRequest(OrderDto Order);
public record CreateOrderResponse(Guid Id);
public class Create : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (ISender sender, CreateOrderRequest request) =>
        {
            var command = request.Adapt<Command>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateOrderResponse>();

            return Results.Created($"/orders/{response.Id}", response);
        })
        .WithName("CreateOrder")
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Order")
        .WithDescription("Create Order");
    }
}
