namespace Catalog.API.Products.Delete;

public record Command(Guid Id) : ICommand<Result>;
public record Result(bool IsSuccess);

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}

internal class Handler(IDocumentSession session, ILogger<Handler> logger) : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Products Delete Handler called with {@command}", command);

        var entity = await session.LoadAsync<Product>(command.Id);
        if(entity is null)
        {
            logger.LogWarning("Product with Id {Id} not found", command.Id);
            throw new ProductNotFoundException(command.Id);
            //return new Result(false);
        }

        session.Delete(entity);

        // save to database
        await session.SaveChangesAsync(cancellationToken);

        // return result
        return new Result(true);
    }
}
