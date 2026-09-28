using MediatR;
using TEcomerc.Application.Commands;
using TEcomerc.Application.Observability;
using TEcomerc.Application.Persistency;
using TEcomerc.Domain.Entities;

namespace TEcomerc.Application.Handlers;

public sealed class ReadOrderByIdHandler : IRequestHandler<ReadOrderByIdCommand, OrderEntity?>
{
    readonly OrderRepository orderRepository;
    readonly ILogger<ReadOrderByIdHandler>? log;


    public ReadOrderByIdHandler(OrderRepository orderItemRepository, ILogger<ReadOrderByIdHandler>? log)
    {
        this.orderRepository = orderItemRepository;
        this.log = log;
    }

    public Task<OrderEntity?> Handle(ReadOrderByIdCommand request, CancellationToken cancellationToken)
    {
        log?.Info($"Lendo order `{request.Id}`");
        return orderRepository.Read(request.Id, cancellationToken);
    }
}