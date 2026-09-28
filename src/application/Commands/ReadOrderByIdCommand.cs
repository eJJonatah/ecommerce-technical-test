using MediatR;
using TEcomerc.Domain.Entities;

namespace TEcomerc.Application.Commands;

public readonly record struct ReadOrderByIdCommand(Guid Id) : IRequest<OrderEntity?>;