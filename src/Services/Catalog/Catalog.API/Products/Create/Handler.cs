namespace Catalog.API.Products.Create;

public record Command(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<Result>;
public record Result(Guid Id);

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
    }
}
internal class Handler(
    IDocumentSession session
//    ILogger<Handler> logger
    ) : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        //logger.LogInformation("Creating product with name: {Name}, category: {Category}, description: {Description}, imageFile: {ImageFile}, price: {Price}",
        //    command.Name, string.Join(", ", command.Category), command.Description, command.ImageFile, command.Price);
        //logger.LogInformation("Create Product Command: {@Command}", command);

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
