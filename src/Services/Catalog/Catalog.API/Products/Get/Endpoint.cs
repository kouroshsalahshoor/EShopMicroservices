namespace Catalog.API.Products.Get;

//public record Request();
public record Response(IEnumerable<Product> Items);
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products",
            async (ISender sender) =>
            {
                var result = await sender.Send(new Query());

                var response = result.Adapt<Response>();

                return Results.Ok(response);
            })
        .WithName("GetProducts")
        .Produces<Response>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}
