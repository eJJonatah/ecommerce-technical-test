namespace TEcomerc.Tests.Handlers;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using TEcomerc.Application.Handlers;
using TEcomerc.Domain.Entities;
using TEcomerc.Domain.ValueObjects;
using TEcomerc.Tests.Resources;
using Xunit.Abstractions;

public sealed class TestCreateOrderHandler : IAsyncLifetime
{
    readonly DatabaseFixture databaseLifetime = new();
    CreateOrderHandler cancelOrderHandler = default!;

    public async Task InitializeAsync()
    {
        await databaseLifetime.InitializeAsync();
        var sp = ServiceCollectionProvider.CreateServiceCollection();
        cancelOrderHandler = ActivatorUtilities.CreateInstance<CreateOrderHandler>(sp);
    }

    [Fact] public async Task PASS_ValidCreate()
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

        _= await cancelOrderHandler.Handle(new(orderEntity), default);

        Assert.True(true);
    }

    [Fact] public async Task FAIL_InvalidCreate()
    {
        if (System.Diagnostics.Debugger.IsAttached) { System.Diagnostics.Debugger.Break(); }

        var check = Assert.ThrowsAnyAsync<Exception>(() =>
            cancelOrderHandler.Handle(new(new OrderValues<OrderItem>(
                Id: Guid.NewGuid(),
                CustomerId: Guid.NewGuid(),
                Status: OrderStatus.Pending,
                CreatedAt: DateTime.Now,
                Items: []
            )), default)
        );

        _= await check;
    }

    public async Task DisposeAsync()
    {
        await databaseLifetime.DisposeAsync();
    }
}