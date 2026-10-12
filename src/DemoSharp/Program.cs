using DemoSharp;

var builder = WebApplication.CreateBuilder(args);

// Configuración de OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// Exponer la especificación OpenAPI
app.MapOpenApi();

// Endpoint de Health Check
app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }))
    .WithName("HealthCheck")
    .WithSummary("Health check endpoint")
    .WithDescription("Retorna el estado de salud de la aplicación");

app.MapGet("/healthz", () => Results.Ok(new { status = "Healthy" }))
    .ExcludeFromDescription();

// Endpoint de Echo
app.MapGet("/echo", (string? message) => Results.Ok(new { message = message ?? string.Empty }))
    .WithName("Echo")
    .WithSummary("Echo endpoint")
    .WithDescription("Retorna el mensaje recibido por parámetro");

app.MapGet("/echo/{message}", (string message) => Results.Ok(new { message }))
    .ExcludeFromDescription();

app.Logger.LogInformation("{Greeting}", Greeting.GetMessage());

app.Run();

public partial class Program { }

