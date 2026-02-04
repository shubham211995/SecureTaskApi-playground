using System.Net;
using Xunit;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;

public class AuthTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Secured_endpoint_without_token_returns_401()
    {
        var response = await _client.GetAsync("/api/tasks");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}