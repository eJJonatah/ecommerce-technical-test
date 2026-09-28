#pragma warning disable CS1591 // Documentações reduntantes

namespace TEcomerc.Domain.Entities;
using System.Collections.Generic;
using TEcomerc.Domain.Exceptions;
using TEcomerc.Domain.ValueObjects;

public interface Order<out TItems> where TItems : OrderItem
{
    Guid Id { get; }
    Guid CustomerId { get; }
    OrderStatus Status { get; }
    DateTime CreatedAt { get; }
    IEnumerable<TItems> Items { get; }

    /// <summary>/!\ Causará a enumeração dos items!</summary>
    decimal TotalAmount => Items.Sum(each => each.UnitPrice * each.Quantity);
}

public sealed record OrderValues<TItems>
(
    Guid Id,
    Guid CustomerId,
    OrderStatus Status,
    DateTime CreatedAt,
    IEnumerable<TItems> Items

)   : Order<TItems>
      where TItems : OrderItem
;


public sealed class OrderEntity : Order<OrderItem>
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public IEnumerable<OrderItemEntity> Items { get; private set; } = default!;
    IEnumerable<OrderItem> Order<OrderItem>.Items => Items;

    /// <summary> /!\ Pode causar a enumeração dos itens! em caso de ´o.Items´ não ser uma coleção </summary>
    public static OrderEntity Create<TItems>(in Order<TItems> o) where TItems : OrderItem
    {
        ArgumentNullException.ThrowIfNull(o);
        InvalidDomainParameterException.RaiseAt(o.Id, IF: Guid.Empty.Equals, "Não pode ser um valor nulo ou zero");
        InvalidDomainParameterException.RaiseAt(o.CustomerId, IF: Guid.Empty.Equals, "Não pode ser um valor nulo ou zero");
        InvalidDomainParameterException.RaiseAt(o.CreatedAt, IF: default(DateTime).Equals, "Não pode ser um valor vazio ou padrão");

        TItems[]? enumeratedItems = null;

        _ = o.Items.TryGetNonEnumeratedCount(out var o_Items_Length)? 0
          : o_Items_Length = (enumeratedItems = [.. o.Items]).Length;

        if (enumeratedItems is not IEnumerable<OrderItemEntity> assCollection)
		{
            assCollection = [.. enumeratedItems is not null
                ? enumeratedItems.Select(OrderItemEntity.Create)
                : o.Items.Select(x => OrderItemEntity.Create(x))];
		}

		InvalidDomainParameterException.RaiseAt(o_Items_Length, 0.Equals, "Um pedido não pode conter item nenhum");

        return new() {
            Id = o.Id,
            CustomerId = o.CustomerId,
            Status = o.Status,
            CreatedAt = o.CreatedAt,
            Items = assCollection
        };

    }

    public OrderValues<OrderItemValues> ToValues()
    {
        return new(
            Id: Id,
            CustomerId: CustomerId,
            Status: Status,
            CreatedAt: CreatedAt,
            Items: Items?.Select(each => each.ToValues())!
        );
    }
}