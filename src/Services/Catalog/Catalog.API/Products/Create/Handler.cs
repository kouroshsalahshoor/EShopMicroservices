using FluentValidation;

namespace Catalog.API.Products.Create;

public record Command(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<Result>;
public record Result(Guid Id);

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}
internal class Handler(
    IDocumentSession session,
    IValidator<Command> validator
    ) : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        // validate command
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

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
