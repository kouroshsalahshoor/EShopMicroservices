namespace Catalog.API.Products.Create;

public record Command(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<Result>;
public record Result(Guid Id);

internal class Handler(IDocumentSession session) : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        // create entity from command object
        var entity = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // save to database
        session.Store(entity);
        await session.SaveChangesAsync(cancellationToken);

        // return result
        return new Result(entity.Id);
    }
}
