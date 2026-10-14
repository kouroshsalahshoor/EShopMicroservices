namespace Ordering.Application.Orders.Commands.Create;

public record Command(OrderDto Order) : ICommand<Result>;
public record Result(Guid Id);

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
    }
}