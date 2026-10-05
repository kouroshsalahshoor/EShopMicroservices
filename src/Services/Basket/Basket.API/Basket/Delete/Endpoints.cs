namespace Basket.API.Basket.Delete;

//public record Request(string UserName);
public record Response(bool IsSuccess);

public class Endpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (ISender sender, string userName) =>
        {
            var result = await sender.Send(new Command(userName));

            var response = result.Adapt<Response>();

            return Results.Ok(response);
        })
        .WithName("DeleteBasket")
        .Produces<Response>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Basket")
        .WithDescription("Delete Basket");
    }
}