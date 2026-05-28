# Agenda API - Sistema de Reservas

API RESTful para la gestión de reservas y agenda de servicios, construida con .NET 8 siguiendo principios de Clean Architecture.

## Tecnologías Utilizadas

- .NET 8: Framework principal.
- ASP.NET Core Web API: Para la exposición de endpoints.
- Entity Framework Core: ORM para acceso a datos.
- PostgreSQL (Supabase): Base de datos relacional.
- Hangfire: Procesamiento de trabajos en segundo plano (limpieza de reservas expiradas).
- FluentValidation: Validaciones complejas de modelos.
- xUnit + Moq: Pruebas unitarias e integración.
- Mapster: Mapeo entre Entidades y DTOs.

## Arquitectura

El proyecto sigue una estructura de Clean Architecture dividida en cuatro capas:

1. Core: Contiene las entidades de dominio, interfaces y reglas de negocio puras. No tiene dependencias externas.
2. Application: Implementa los casos de uso (CreateReservation, GetAvailability), validaciones y lógica de aplicación.
3. Infrastructure: Implementación de detalles externos (EF Core, SendGrid, Stripe, Hangfire).
4. API: Capa de presentación (Controladores) y configuración de la aplicación.

## Estructura del Proyecto

AgendaAPI/
|-- src/
|   |-- AgendaAPI.Core/          # Dominio y Reglas de Negocio
|   |-- AgendaAPI.Application/   # Casos de Uso y Validaciones
|   |-- AgendaAPI.Infrastructure/# Persistencia y Servicios Externos
|   |-- AgendaAPI.API/           # Entry Point (Web API)
|-- tests/
    |-- AgendaAPI.UnitTests/     # Pruebas Unitarias

## Configuración Inicial

1. Clonar el repositorio.
2. Restaurar paquetes NuGet:
   dotnet restore

3. Configurar variables de entorno en appsettings.Development.json o usando User Secrets:
   - ConnectionStrings:DefaultConnection
   - SendGrid:ApiKey
   - Stripe:SecretKey

## Ejecutar Tests

dotnet test

## Objetivos de Aprendizaje

- Implementación de Result Pattern para manejo de errores funcional.
- Uso de Background Jobs con Hangfire para tareas automáticas.
- Validaciones robustas con FluentValidation.
- Pruebas unitarias con xUnit y Moq.
