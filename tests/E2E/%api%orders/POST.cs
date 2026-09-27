namespace TEcomerc.Tests.E2e;
using TEcomerc.Tests.Resources;

public sealed partial class ApiOrders : IAsyncLifetime
{
    HttpClient _client = default!;

    public async Task InitializeAsync() { _client = await TestHelpers.AuthenticateClient(new()); }

    [Fact] public async Task POST_NewOrder()
    {
        Assert.Fail("Not implemented");
    }

    [Fact] public async Task POST_Unauthorized()
    {
        Assert.Fail("Not implemented");
    }

    public Task DisposeAsync()
    {
        _client.Dispose();
        return Task.CompletedTask;
    }

}