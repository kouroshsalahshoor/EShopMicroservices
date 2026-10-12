namespace Ordering.Domain.Events;

public record OrderCreatedEvent(Order order) : IDomainEvent;
public record OrderUpdatedEvent(Order order) : IDomainEvent;
public record OrderDeletedEvent(Order order) : IDomainEvent;