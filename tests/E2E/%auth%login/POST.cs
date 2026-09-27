namespace TEcomerc.Tests.E2e;

using System.Net.Http.Json;
using TEcomerc.Tests.Resources;

public sealed class AuthLogin : IDisposable
{
    readonly HttpClient _client;


    public AuthLogin() { _client = new HttpClient(); }

    [Fact] public async Task POST_GetJwt()
    {
        var response = await _client.PostAsJsonAsync(RouteFormats.AUTH_LOGIN, """
        {
            "user": "dev@martech.com",
            "password":"Senha@123"
        }
        """);

        _= response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("JWT", content, StringComparison.OrdinalIgnoreCase);
    }

    public void Dispose() { _client.Dispose(); }

}