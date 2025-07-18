using Microsoft.EntityFrameworkCore;

namespace ToDoListOnOff.Infraestructure.Persistence.Context.Factory;

/// <summary>
/// ToDoListContextFactory
/// </summary>
/// <param name="options">Opciones del dbcontext</param>
public class ToDoListContextFactory(DbContextOptions<ToDoListContext> options) : IDbContextFactory<ToDoListContext>
{
    /// <summary>
    /// Metodo que crea un nuevo contexto
    /// </summary>
    /// <returns></returns>
    public ToDoListContext CreateDbContext() => new ToDoListContext(options);
}