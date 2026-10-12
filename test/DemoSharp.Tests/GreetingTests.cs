using DemoSharp;

namespace DemoSharp.Tests;

/// <summary>
/// Pruebas unitarias para la clase Greeting.
/// </summary>
public class GreetingTests
{
    /// <summary>
    /// Verifica que GetMessage retorne el saludo esperado "Hello, World!".
    /// </summary>
    [Fact]
    public void GetMessage_ReturnsHelloWorld()
    {
        // Act & Assert: Validar que el mensaje obtenido corresponda al esperado
        Assert.Equal("Hello, World!", Greeting.GetMessage());
    }
}
