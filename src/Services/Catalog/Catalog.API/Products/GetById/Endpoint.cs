namespace Catalog.API.Products.GetById;

public record Request(Guid Id);
public record Response(Product Item);
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}",
            async (ISender sender, Guid id) =>
            {
                var result = await sender.Send(new Query(id));

                var response = result.Adapt<Response>();

                return Results.Ok(response);
            })
        .WithName("GetProductById")
        .Produces<Response>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}
