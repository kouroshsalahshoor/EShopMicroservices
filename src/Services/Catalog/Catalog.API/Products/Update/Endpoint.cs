namespace Catalog.API.Products.Update;

public record Request(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
public record Response(bool IsSuccess);
public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products",
            async (Request request, ISender sender) =>
            {
                var command = request.Adapt<Command>();

                var result = await sender.Send(command);

                var response = result.Adapt<Response>();

                return Results.Ok(response);

            })
        .WithName("UpdateProduct")
        .Produces<Response>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}
