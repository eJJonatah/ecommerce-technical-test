using Microsoft.AspNetCore.Mvc.Testing;
namespace TEcomerc.Tests.E2e;

using System.Net.Http.Json;
using TEcomerc.Tests.Resources;

public sealed class AuthLogin : IAsyncLifetime, IClassFixture<WebApplicationFactory<Program>>
{
	readonly WebApplicationFactory<Program> webappFactory;
    HttpClient api = default!;

	public AuthLogin(WebApplicationFactory<Program> webApplicationFactory)
        => webappFactory = webApplicationFactory;

	public Task InitializeAsync() {
        return Task.Run(() => api = webappFactory.CreateDefaultClient());
    }

	public Task DisposeAsync()
	{
        api.Dispose();
        return Task.CompletedTask;
	}

	[Fact] public async Task POST_GetJwt()
    {
        if (System.Diagnostics.Debugger.IsAttached) { System.Diagnostics.Debugger.Break(); }

        var response = await api.PostAsJsonAsync(RouteFormats.AUTH_LOGIN, new
        {
            user = "dev@martech.com",
            password = "Senha@123"
        });

        _= response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("JWT", content, StringComparison.OrdinalIgnoreCase);
    }

    [Fact] public async Task POST_Unauthorized()
    {
        if (System.Diagnostics.Debugger.IsAttached) { System.Diagnostics.Debugger.Break(); }

        var response = await api.PostAsJsonAsync(RouteFormats.AUTH_LOGIN, new
        {
            user = "dev@martech.com",
            password = "Senha@122"
        });

        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }
}