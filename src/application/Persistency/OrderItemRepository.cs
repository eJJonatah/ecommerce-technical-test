using TEcomerc.Domain.Entities;
namespace TEcomerc.Application.Persistency;


public interface OrderItemRepository
{
    Task<OrderItem?> Read(Guid id, CancellationToken ct);
    Task<Guid> Add(OrderItem instance);
}
