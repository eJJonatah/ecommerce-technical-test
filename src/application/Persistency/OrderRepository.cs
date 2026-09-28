using TEcomerc.Application.Commands;
using TEcomerc.Domain.Entities;
namespace TEcomerc.Application.Persistency;


public interface OrderRepository
{
    Task Update<TItem>(Guid id, RepositoryCommandUpdate<Order<TItem>> update) where TItem : OrderItem;

    IAsyncEnumerable<OrderEntity> List(ListOrderQuery query, CancellationToken ct);
    Task<OrderEntity?> Read(Guid id, CancellationToken ct);
    Task<Guid> Add<TItem>(Order<TItem> instance) where TItem : OrderItem;
}
