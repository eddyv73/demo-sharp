# demo-sharp
Este repositorio contiene una aplicación de demostración para programación en vivo y permite comprobar las pruebas unitarias.
Estructura para un proyecto C# con .NET y ASP.NET Core:

```text
doc/                  Documentación del proyecto
src/DemoSharp/        Aplicación Web API con OpenAPI
test/DemoSharp.Tests/ Pruebas automatizadas
demo-sharp.sln        Solución de .NET
```

## Requisitos

- .NET 10 SDK

## Uso

Desde la raíz del repositorio:

```bash
dotnet build
dotnet run --project src/DemoSharp
dotnet test
```

## Endpoints

- **OpenAPI**: `GET /openapi/v1.json` - Especificación OpenAPI de la API.
- **Health Check**: `GET /health` - Retorna el estado de salud del servicio (`{"status": "Healthy"}`).
- **Echo**: `GET /echo?message={texto}` o `GET /echo/{texto}` - Retorna el mensaje recibido (`{"message": "..."}`).



La estructura de carpetas está basada en buenas prácticas de la empresa.

Este proyecto está bajo la licencia Apache 2.0.
