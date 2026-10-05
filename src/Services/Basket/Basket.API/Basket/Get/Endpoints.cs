namespace Basket.API.Basket.Get;

//public record Request(string UserName); 
public record Response(Cart Cart);

public class GetBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (ISender sender, string userName) =>
        {
            var result = await sender.Send(new Query(userName));

            var response = result.Adapt<Response>();

            return Results.Ok(response);
        })
        .WithName("GetBasket")
        .Produces<Response>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Basket By User Name")
        .WithDescription("Get Basket By User Name");
    }
}