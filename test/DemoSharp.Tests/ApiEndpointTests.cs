using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DemoSharp.Tests;

public class ApiEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task HealthCheck_ReturnsHealthyStatus()
    {
        var response = await _client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        Assert.Equal("Healthy", doc.RootElement.GetProperty("status").GetString());
    }

    [Fact]
    public async Task Echo_WithQueryParameter_ReturnsEchoedMessage()
    {
        var response = await _client.GetAsync("/echo?message=HelloCopilot");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        Assert.Equal("HelloCopilot", doc.RootElement.GetProperty("message").GetString());
    }

    [Fact]
    public async Task Echo_WithRouteParameter_ReturnsEchoedMessage()
    {
        var response = await _client.GetAsync("/echo/HelloRoute");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        Assert.Equal("HelloRoute", doc.RootElement.GetProperty("message").GetString());
    }

    [Fact]
    public async Task Echo_WithoutParameter_ReturnsEmptyMessage()
    {
        var response = await _client.GetAsync("/echo");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        Assert.Equal(string.Empty, doc.RootElement.GetProperty("message").GetString());
    }

    [Fact]
    public async Task OpenApi_ReturnsValidSpecificationWithEndpoints()
    {
        var response = await _client.GetAsync("/openapi/v1.json");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(content);
        var paths = doc.RootElement.GetProperty("paths");

        Assert.True(paths.TryGetProperty("/health", out _));
        Assert.True(paths.TryGetProperty("/echo", out _));
    }
}
