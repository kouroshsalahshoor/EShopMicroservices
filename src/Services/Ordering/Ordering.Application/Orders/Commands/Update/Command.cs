namespace Ordering.Application.Orders.Commands.Update;

public record Command(OrderDto Order) : ICommand<Result>;
public record Result(bool IsSuccess);

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Order.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
    }
}