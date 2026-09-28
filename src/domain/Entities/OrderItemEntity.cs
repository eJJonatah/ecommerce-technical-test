#pragma warning disable CS1591 // Documentações reduntantes

namespace TEcomerc.Domain.Entities;
using TEcomerc.Domain.Exceptions;

public interface OrderItem
{
    Guid Id { get; }
    Guid OrderId { get; }
    string ProductName { get; }
    int Quantity { get; }
    decimal UnitPrice { get; }
}


public readonly record struct OrderItemValues
(
    Guid Id,
    Guid OrderId,
    string ProductName,
    int Quantity,
    decimal UnitPrice
) : OrderItem;


public sealed class OrderItemEntity : OrderItem
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public string ProductName { get; private set; } = default!;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }


    public static OrderItemEntity Create<TOrderItem>(TOrderItem o) where TOrderItem : OrderItem
    {
        InvalidDomainParameterException.RaiseAt(o.Id, IF: Guid.Empty.Equals, "Cannot be empty or default");
        InvalidDomainParameterException.RaiseAt(o.OrderId, IF: Guid.Empty.Equals, "Cannot be empty or default");
        InvalidDomainParameterException.RaiseAt(o.ProductName, IF: string.IsNullOrWhiteSpace, "Product name cannot be null or empty or white space");
        InvalidDomainParameterException.RaiseAt(o.Quantity, IF: n => n <= 0, "Cannot be 0 or negative");
        InvalidDomainParameterException.RaiseAt(o.UnitPrice, IF: n => n <= 0, "Cannot be 0 or negative");

        return new() {
            Id = o.Id,
            OrderId = o.OrderId,
            ProductName = o.ProductName,
            Quantity = o.Quantity,
            UnitPrice = o.UnitPrice,
        };

    }

	public OrderItemValues ToValues()
	{
        return new(
            Id: Id,
            OrderId: OrderId,
            ProductName: ProductName,
            Quantity: Quantity,
            UnitPrice: UnitPrice
        );
	}
}