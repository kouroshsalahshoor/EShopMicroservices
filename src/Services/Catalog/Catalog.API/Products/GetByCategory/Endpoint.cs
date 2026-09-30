namespace Catalog.API.Products.GetByCategory;

public record Request(string Category);
public record Response(IEnumerable<Product> Items);
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}",
            async (ISender sender, string category) =>
            {
                var result = await sender.Send(new Query(category));

                var response = result.Adapt<Response>();
                
                return Results.Ok(response);
            })
        .WithName("GetProductByCategory")
        .Produces<Response>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category");
    }
}
