namespace Catalog.API.Products.Update;

public record Command(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<Result>;
public record Result(bool IsSuccess);

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
internal class Handler(
    IDocumentSession session 
    //ILogger<Handler> logger
    ) 
    : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        //logger.LogInformation("Products Update Handler called with {@command}", command);

        var entity = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (entity is null)
        {
            //logger.LogWarning("Product with Id {Id} not found", command.Id);
            throw new ProductNotFoundException(command.Id);
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
