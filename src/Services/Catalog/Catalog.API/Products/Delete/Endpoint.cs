namespace Catalog.API.Products.Delete;

//public record Request(Guid Id);
public record Response(bool IsSuccess);
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}",
            async (ISender sender, Guid id) =>
            {
                var command = new Command(id);
                var result = await sender.Send(command);

                var response = result.Adapt<Response>();
                return Results.Ok(response);

            })
        .WithName("DeleteProduct")
        .Produces<Response>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Product")
        .WithDescription("Delete Product");
    }
}
