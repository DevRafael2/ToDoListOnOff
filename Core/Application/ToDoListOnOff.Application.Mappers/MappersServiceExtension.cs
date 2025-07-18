using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoListOnOff.Application.Mappers;

/// <summary>
/// Clase extensiva para agregar mapeos al contenedor de dependencias
/// </summary>
public static class MappersServiceExtension
{
    /// <summary>
    /// Metodo extensivo para agregar mapeos al contenedor de dependencias
    /// </summary>
    /// <param name="services">Contenedor de dependencias</param>
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddMapster();
        return services;
    }
}