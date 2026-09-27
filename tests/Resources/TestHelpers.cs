using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace TEcomerc.Tests.Resources;

static class TestHelpers
{
    public static async Task<HttpClient> AuthenticateClient(HttpClient client)
    {
        var response = await client.PostAsJsonAsync(RouteFormats.AUTH_LOGIN, """
        {
            "user": "dev@martech.com",
            "password":"Senha@123"
        }
        """).ConfigureAwait(false);

        var content = await response.Content.ReadFromJsonAsync<dynamic>().ConfigureAwait(false);

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer", content!.JWT
        );

        return client;
    }
}