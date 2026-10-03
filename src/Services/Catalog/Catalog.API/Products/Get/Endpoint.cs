namespace Catalog.API.Products.Get;

public record Request(int? PageNumber = 1, int? PageSize = 10);
public record Response(IEnumerable<Product> Items);
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products",
            async (ISender sender, [AsParameters] Request request) =>
            {
                var query = request.Adapt<Query>();
                var result = await sender.Send(query);
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
