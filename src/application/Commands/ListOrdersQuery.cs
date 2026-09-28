using MediatR;
using TEcomerc.Domain.Entities;

namespace TEcomerc.Application.Commands;

public readonly record struct ListOrderQuery(int Page, int PageSize, bool IncludeItems)
    : IRequest<IAsyncEnumerable<OrderEntity>>;