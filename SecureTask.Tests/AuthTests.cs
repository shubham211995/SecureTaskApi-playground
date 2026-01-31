using System.Net;
using System.Net.Http.Headers;
using Xunit;

public class AuthTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Protected_endpoint_without_token_returns_401()
    {
        var response = await _client.GetAsync("/api/tasks");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task User_with_valid_role_can_access()
    {
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", TestJwtTokens.UserToken);
    
        var response = await _client.GetAsync("/api/tasks");
    
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}