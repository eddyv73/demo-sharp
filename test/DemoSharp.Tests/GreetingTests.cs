using DemoSharp;

namespace DemoSharp.Tests;

public class GreetingTests
{
    [Fact]
    public void GetMessage_ReturnsHelloWorld()
    {
        Assert.Equal("Hello, World!", Greeting.GetMessage());
    }
}
