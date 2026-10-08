
using Discount.Grpc;

namespace Basket.API.Basket.Save;

public record Command(Cart Cart) : ICommand<Result>;
public record Result(string UserName);

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class Handler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountClient)
    : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);

        await repository.Save(command.Cart, cancellationToken);

        return new Result(command.Cart.UserName);
    }

    private async Task DeductDiscount(Cart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountClient.GetByNameAsync(new GetByNameRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}