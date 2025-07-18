using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ToDoListOnOff.Application.UseCases.Behaviors;

namespace ToDoListOnOff.Application.UseCases;

/// <summary>
/// Clase extensiva para agregar los casos de uso al service collection
/// </summary>
public static class UseCasesServiceExtension
{
    /// <summary>
    /// Metodo extensivo para agregar casos de uso al service collection
    /// </summary>
    /// <param name="services">Contenedor de dependencias</param>
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddMediatR(e => 
            e.RegisterServicesFromAssembly(typeof(UseCasesServiceExtension).Assembly));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PipeValidator<,>));
        return services;
    }
}