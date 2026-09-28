using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace TEcomerc.Tests.Resources;

static class TestHelpers
{
    public static async Task<HttpClient> Login(HttpClient client)
    {
        var response = await client.PostAsJsonAsync(RouteFormats.AUTH_LOGIN, new
        {
            user = "dev@martech.com",
            password = "Senha@123"
        });

        var content = await response.Content.ReadFromJsonAsync<JsonElement>().ConfigureAwait(false);

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer", content!.GetProperty("jwt").GetString()
        );

        return client;
    }
}