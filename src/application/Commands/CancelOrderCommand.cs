using MediatR;
using TEcomerc.Domain.Entities;

namespace TEcomerc.Application.Commands;

public readonly record struct CancelOrderCommand(Guid Id) : IRequest;