using AgendaAPI.Application.Features.Reservations.CreateReservation;
using AgendaAPI.Application.Features.Reservations.GetReservationById;
using AgendaAPI.Application.Features.Reservations.GetReservationsByClient;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AgendaAPI.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        // Registrar el Handler
        services.AddScoped<CreateReservationHandler>();
        services.AddScoped<GetReservationByIdHandler>();
        services.AddScoped<GetReservationsByClientHandler>();

        return services;
    }
}