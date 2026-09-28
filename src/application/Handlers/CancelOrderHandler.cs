using TEcomerc.Domain.Entities;
using TEcomerc.Domain.ValueObjects;
namespace TEcomerc.Application.Handlers;

using MediatR;
using TEcomerc.Application.Commands;
using TEcomerc.Application.Observability;
using TEcomerc.Application.Persistency;

public sealed class CancelOrderHandler : IRequestHandler<CancelOrderCommand>
{
    readonly OrderRepository orderRepository;
    readonly ILogger<CancelOrderHandler>? log;

    public CancelOrderHandler(OrderRepository orderRepository, ILogger<CancelOrderHandler>? log)
    {
        this.orderRepository = orderRepository;
        this.log = log;
    }

    public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        log?.Info($"Atualizando status da ordem `{request}` para: Cancelado");

        var updatedOrder = await orderRepository.Read(request.Id, cancellationToken) ?? throw new ArgumentOutOfRangeException($"""
            Não é possível cancelar a ordem `{request}` pois ela não existe
            """);

        if (updatedOrder.Status is not OrderStatus.Pending)
        {
            throw new ArgumentOutOfRangeException($"""
                Não é possível cancelar a ordem `{request}` pois ela está com o estatus `{updatedOrder.Status}`
                """);
        }

        await orderRepository.Update(request.Id, new RepositoryCommandBuilderUpdate<Order<OrderItem>>()
            .Change(old => old.Status, to: OrderStatus.Cancelled)
            .AsChanges());
    }
}