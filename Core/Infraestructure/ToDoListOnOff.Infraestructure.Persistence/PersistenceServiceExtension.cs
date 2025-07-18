using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoListOnOff.Domain.Interfaces.Repositories;
using ToDoListOnOff.Infraestructure.Persistence.Context;
using ToDoListOnOff.Infraestructure.Persistence.Repositories;
using ToDoListOnOff.Transversal.Helpers.Constants;

namespace ToDoListOnOff.Infraestructure.Persistence;

/// <summary>
/// Clase extensiva para agregar persistencia
/// </summary>
public static class PersistenceServiceExtension
{
    /// <summary>
    /// Metodo extensivo para ServiceCollection 
    /// </summary>
    /// <param name="services">Contenedor de dependencias</param>
    /// <param name="configuration">Configuración</param>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string? dbConnection = configuration.GetConnectionString(ApplicationConstants.AppDbConnectionKey);
        Action<DbContextOptionsBuilder> optionsBuilder = options =>
        {
            options.UseSqlServer(dbConnection, 
                e => e.MigrationsAssembly("TodoListOnOff.Api"));
#if DEBUG
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
#endif
        };
        
        
        services.AddDbContext<ToDoListContext>(optionsBuilder);
        services.AddDbContextFactory<ToDoListContext>(optionsBuilder, ServiceLifetime.Scoped);

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        return services;
    }
}