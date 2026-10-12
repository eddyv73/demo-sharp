# demo-sharp

Estructura inicial para un proyecto C# con .NET:

```text
doc/                  Documentación del proyecto
src/DemoSharp/        Aplicación de consola
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