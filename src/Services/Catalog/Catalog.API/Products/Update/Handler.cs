namespace Catalog.API.Products.Update;

public record Command(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<Result>;
public record Result(bool IsSuccess);

internal class Handler(IDocumentSession session, ILogger<Handler> logger) : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Products Update Handler called with {@command}", command);

        var entity = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (entity is null)
        {
            logger.LogWarning("Product with Id {Id} not found", command.Id);
            throw new NotFoundException();
            //return new Result(false);
        }

        // update entity properties
        entity.Name = command.Name;
        entity.Category = command.Category;
        entity.Description = command.Description;
        entity.ImageFile = command.ImageFile;
        entity.Price = command.Price;

        // save to database
        session.Update(entity);
        await session.SaveChangesAsync(cancellationToken);

        // return result
        return new Result(true);
    }
}
