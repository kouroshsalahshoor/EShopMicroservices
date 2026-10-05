namespace Basket.API.Basket.Save;

public record Request(Cart Cart);
public record Response(string UserName);

public class StoreBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (ISender sender, Request request) =>
        {
            var command = request.Adapt<Command>();

            var result = await sender.Send(command);

            var response = result.Adapt<Response>();

            return Results.Created($"/basket/{response.UserName}", response);
        })
        .WithName("SaveBasket")
        .Produces<Response>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Save Basket")
        .WithDescription("Save Basket");
    }
}