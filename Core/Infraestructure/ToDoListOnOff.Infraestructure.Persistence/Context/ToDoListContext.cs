using Microsoft.EntityFrameworkCore;
using ToDoListOnOff.Infraestructure.Configuration;

namespace ToDoListOnOff.Infraestructure.Persistence.Context;

/// <summary>
/// Contexto de base de datos
/// </summary>
public class ToDoListContext(DbContextOptions<ToDoListContext> options) : DbContext(options)
{
    /// <summary>
    /// Configuración de modelbuilder
    /// </summary>
    /// <param name="modelBuilder">Modelbuilder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoListConfigurationAssemblyReference).Assembly);
    }
}