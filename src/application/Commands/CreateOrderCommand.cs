using MediatR;
using TEcomerc.Domain.Entities;

namespace TEcomerc.Application.Commands;

public readonly record struct CreateOrderCommand(Order<OrderItem> Order) : IRequest<Guid>;