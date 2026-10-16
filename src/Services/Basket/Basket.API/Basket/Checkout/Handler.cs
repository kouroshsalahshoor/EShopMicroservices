
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.Checkout;

public record Command(CheckoutDto dto) : ICommand<Result>;
public record Result(bool IsSuccess);

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.dto).NotNull().WithMessage("CheckoutDto can't be null");
        RuleFor(x => x.dto.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class Handler(IBasketRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        var basket = await repository.Get(command.dto.UserName, cancellationToken);
        if (basket == null)
        {
            return new Result(false);
        }

        var eventMessage = command.dto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        //dual write problem!!! => solution: Transactional Outbox Pattern
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.Delete(command.dto.UserName, cancellationToken);

        return new Result(true);
    }
}