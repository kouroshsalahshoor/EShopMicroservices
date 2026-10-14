namespace Ordering.Application.Orders.Commands.Delete;

public record Command(Guid OrderId) : ICommand<Result>;
public record Result(bool IsSuccess);

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required");
    }
}