#pragma warning disable CA1515 // Consider making public types internal

namespace TEcomerc.Tests.E2e;

using Microsoft.AspNetCore.Mvc.Testing;
using TEcomerc.Domain.ValueObjects;
using TEcomerc.Domain.Entities;
using TEcomerc.Tests.Resources;
using System.Net.Http.Json;

public sealed class GetIdApiOrders : IAsyncLifetime, IClassFixture<WebApplicationFactory<Program>>
{
    HttpClient api = default!;
    HttpClient apiLogout = default!;
    readonly DatabaseFixture databaseLifetime = new();
    readonly WebApplicationFactory<Program> webappFactory;
	public GetIdApiOrders(WebApplicationFactory<Program> webApplicationFactory)
	{
		webappFactory = webApplicationFactory;
	}

    public async Task InitializeAsync()
    {
        await Task.Yield();
        api = webappFactory.CreateDefaultClient();
        apiLogout = webappFactory.CreateDefaultClient();
        api = await TestHelpers.Login(api);
        await databaseLifetime.InitializeAsync();
    }

    [Fact] public async Task GET_OrderById()
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

        var response = await api.PostAsJsonAsync(RouteFormats.API_ORDERS, orderEntity);
        _ = response.EnsureSuccessStatusCode();

        var content = await api.GetFromJsonAsync<OrderValues<OrderItemValues>>(
            string.Format(RouteFormats.FMT_API_ORDERS__ID, orderId)
        );

        Assert.Equal(orderId, content?.Id);
    }

    public async Task DisposeAsync()
    {
        await databaseLifetime.InitializeAsync();
        apiLogout.Dispose();
        api.Dispose();
    }
}