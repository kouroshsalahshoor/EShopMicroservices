namespace Basket.API.Basket.Delete;

public record Command(string UserName) : ICommand<Result>;
public record Result(bool IsSuccess);

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class Handler(IBasketRepository repository)
    : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        // delete basket from database and cache       
        await repository.Delete(command.UserName, cancellationToken);

        return new Result(true);
    }
}