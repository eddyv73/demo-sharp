using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DemoSharp.Tests;

/// <summary>
/// Pruebas de integración para los endpoints de la API (Health Check, Echo y OpenAPI).
/// Utiliza WebApplicationFactory para instanciar la aplicación en memoria.
/// </summary>
public class ApiEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiEndpointTests(WebApplicationFactory<Program> factory)
    {
        // Cliente HTTP configurado para enviar peticiones al servidor en memoria
        _client = factory.CreateClient();
    }

    /// <summary>
    /// Verifica que el endpoint /health responda con código 200 (OK) y estado "Healthy".
    /// </summary>
    [Fact]
    public async Task HealthCheck_ReturnsHealthyStatus()
    {
        // Act: Enviar solicitud GET al endpoint de health check
        var response = await _client.GetAsync("/health");

        // Assert: Validar código de estado exitoso y contenido del JSON de respuesta
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        Assert.Equal("Healthy", doc.RootElement.GetProperty("status").GetString());
    }

    /// <summary>
    /// Verifica que el endpoint /echo responda con el mensaje enviado mediante query string.
    /// </summary>
    [Fact]
    public async Task Echo_WithQueryParameter_ReturnsEchoedMessage()
    {
        // Arrange: Definir mensaje esperado
        const string expectedMessage = "HelloCopilot";

        // Act: Enviar solicitud GET con parámetro de consulta
        var response = await _client.GetAsync($"/echo?message={expectedMessage}");

        // Assert: Validar código 200 y que el mensaje recibido coincida con el enviado
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        Assert.Equal(expectedMessage, doc.RootElement.GetProperty("message").GetString());
    }

    /// <summary>
    /// Verifica que el endpoint /echo responda con el mensaje enviado como parámetro de ruta.
    /// </summary>
    [Fact]
    public async Task Echo_WithRouteParameter_ReturnsEchoedMessage()
    {
        // Arrange: Definir mensaje esperado
        const string expectedMessage = "HelloRoute";

        // Act: Enviar solicitud GET con parámetro de ruta
        var response = await _client.GetAsync($"/echo/{expectedMessage}");

        // Assert: Validar código 200 y que el mensaje recibido coincida con el enviado
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        Assert.Equal(expectedMessage, doc.RootElement.GetProperty("message").GetString());
    }

    /// <summary>
    /// Verifica que el endpoint /echo sin parámetros responda con un mensaje vacío.
    /// </summary>
    [Fact]
    public async Task Echo_WithoutParameter_ReturnsEmptyMessage()
    {
        // Act: Enviar solicitud GET sin parámetros
        var response = await _client.GetAsync("/echo");

        // Assert: Validar código 200 y contenido de mensaje vacío
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        Assert.Equal(string.Empty, doc.RootElement.GetProperty("message").GetString());
    }

    /// <summary>
    /// Verifica que la especificación OpenAPI (/openapi/v1.json) esté disponible y contenga los endpoints definidos.
    /// </summary>
    [Fact]
    public async Task OpenApi_ReturnsValidSpecificationWithEndpoints()
    {
        // Act: Enviar solicitud GET para obtener el documento OpenAPI en formato JSON
        var response = await _client.GetAsync("/openapi/v1.json");

        // Assert: Validar código 200 y presencia de las rutas /health y /echo en el esquema OpenAPI
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(content);
        var paths = doc.RootElement.GetProperty("paths");

        Assert.True(paths.TryGetProperty("/health", out _));
        Assert.True(paths.TryGetProperty("/echo", out _));
    }
}
