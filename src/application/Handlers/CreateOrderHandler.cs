using MediatR;
using System.Collections.ObjectModel;
using TEcomerc.Application.Commands;
using TEcomerc.Application.Observability;
using TEcomerc.Application.Persistency;
using TEcomerc.Domain.Entities;

namespace TEcomerc.Application.Handlers;

public sealed class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    readonly OrderRepository orderRepository;
    readonly OrderItemRepository orderItemRepository;
    readonly ILogger<CreateOrderHandler>? log;


    public CreateOrderHandler(OrderRepository orderRepository,
        OrderItemRepository orderItemRepository,
        ILogger<CreateOrderHandler>? log = null)
    {
        this.orderRepository = orderRepository;
        this.log = log;
        this.orderItemRepository = orderItemRepository;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        log?.Info($"Adicionando ordem: ´{request.Order}´ ao repositório");
        var orderItems = request.Order.Items.Select(OrderItemEntity.Create)
            .Aggregate(new Collection<OrderItemEntity>(), (arr, item) => {
                arr.Add(item);
                return arr;
            });

        var order = new OrderValues<OrderItemEntity>
        (
            Id: request.Order.Id,
            CustomerId: request.Order.CustomerId,
            Status: request.Order.Status,
            CreatedAt: request.Order.CreatedAt,
            Items: orderItems
        );

        var id = await orderRepository.Add(order);
        #pragma warning disable S3267, IDE0011, RCS1001 // Loops should be simplified with "LINQ" expressions

        foreach (var orderItem in orderItems)
			if (await orderItemRepository.Read(orderItem.Id, cancellationToken) is null)
            {   _= await orderItemRepository.Add(orderItem)
            ;}

		return id;
    }
}