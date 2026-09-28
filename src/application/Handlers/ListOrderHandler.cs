using MediatR;
using TEcomerc.Application.Commands;
using TEcomerc.Application.Observability;
using TEcomerc.Application.Persistency;
using TEcomerc.Domain.Entities;

namespace TEcomerc.Application.Handlers;

public sealed class ListOrderHandler : IRequestHandler<ListOrderQuery, IAsyncEnumerable<OrderEntity>>
{
    readonly OrderRepository orderRepository;
    readonly ILogger<ListOrderHandler>? log;


    public ListOrderHandler(OrderRepository orderItemRepository, ILogger<ListOrderHandler>? log)
    {
        this.orderRepository = orderItemRepository;
        this.log = log;
    }

    public Task<IAsyncEnumerable<OrderEntity>> Handle(ListOrderQuery request, CancellationToken cancellationToken)
    {
        log?.Info($"selecionando listagem de ordens pag: `{request.Page}`");
        return Task.FromResult(orderRepository.List(request, cancellationToken));
    }
}