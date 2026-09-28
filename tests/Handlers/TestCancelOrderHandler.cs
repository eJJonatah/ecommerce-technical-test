namespace TEcomerc.Tests.Handlers;

using TEcomerc.Domain.ValueObjects;
using TEcomerc.Domain.Entities;
using TEcomerc.Tests.Resources;
using TEcomerc.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;

public sealed class TestCancelOrderHandler : IAsyncLifetime
{
    readonly DatabaseFixture databaseLifetime = new();
    CancelOrderHandler cancelOrderHandler = default!;

    public async Task InitializeAsync()
    {
        await databaseLifetime.InitializeAsync();
        var sp = ServiceCollectionProvider.CreateServiceCollection();
        cancelOrderHandler = ActivatorUtilities.CreateInstance<CancelOrderHandler>(sp);
    }

    [Fact] public async Task PASS_ValidCancel()
    {
        if (System.Diagnostics.Debugger.IsAttached) { System.Diagnostics.Debugger.Break(); }

        var orderId = Guid.NewGuid();
        var orderEntity = OrderEntity.Create(new OrderValues<OrderItemEntity>
        (
            Id: orderId,
            CustomerId: Guid.NewGuid(),
            Status: OrderStatus.Pending,
            CreatedAt: DateTime.Now,
            Items: [
                OrderItemEntity.Create(new OrderItemValues() {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductName = "Produto A",
                    Quantity = 10,
                    UnitPrice = 25.90m,
                }),

                OrderItemEntity.Create(new OrderItemValues() {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductName = "Produto B",
                    Quantity = 10,
                    UnitPrice = 25.90m,
                }),

                OrderItemEntity.Create(new OrderItemValues() {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductName = "Produto C",
                    Quantity = 10,
                    UnitPrice = 25.90m,
                }),
            ]
        ));

        _= databaseLifetime.Db.Orders.Add(orderEntity);
        _= await databaseLifetime.Db.SaveChangesAsync();

        await cancelOrderHandler.Handle(new(orderId), default);

        Assert.True(true);
    }

    [Fact] public async Task FAIL_InvalidCancel()
    {
        if (System.Diagnostics.Debugger.IsAttached) { System.Diagnostics.Debugger.Break(); }

        var orderId = Guid.NewGuid();
        var orderEntity = OrderEntity.Create(new OrderValues<OrderItemEntity>
        (
            Id: orderId,
            CustomerId: Guid.NewGuid(),
            Status: OrderStatus.Confirmed,
            CreatedAt: DateTime.Now,
            Items: [
                OrderItemEntity.Create(new OrderItemValues() {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductName = "Produto A",
                    Quantity = 10,
                    UnitPrice = 25.90m,
                }),

                OrderItemEntity.Create(new OrderItemValues() {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductName = "Produto B",
                    Quantity = 10,
                    UnitPrice = 25.90m,
                }),

                OrderItemEntity.Create(new OrderItemValues() {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductName = "Produto C",
                    Quantity = 10,
                    UnitPrice = 25.90m,
                }),
            ]
        ));

        _= databaseLifetime.Db.Orders.Add(orderEntity);
        _= await databaseLifetime.Db.SaveChangesAsync();

        var check = Assert.ThrowsAnyAsync<Exception>(() => cancelOrderHandler.Handle(new(orderId), default));

        _= await check;
    }

    public async Task DisposeAsync()
    {
        await databaseLifetime.DisposeAsync();
    }
}