using ToDoListOnOff.Domain.Primitives;

namespace ToDoListOnOff.Domain.Interfaces.Repositories;

/// <summary>
/// Unidad de trabajo
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Metodo que obtiene un repositorio
    /// </summary>
    /// <typeparam name="TEntity">Entidad del repositorio</typeparam>
    /// <typeparam name="TId">Tipo de id de la entidad</typeparam>
    public IRepository<TEntity, TId> GetRepository<TEntity, TId>() where TEntity : EntityRoot<TId>, new();
    /// <summary>
    /// Metodo que obtiene repositorio no generico
    /// </summary>
    /// <typeparam name="TRepository">Tipo del repositorio</typeparam>
    /// <typeparam name="TEntity">Entidad</typeparam>
    /// <typeparam name="TId">Tipo de id</typeparam>
    /// <returns></returns>
    public TRepository GetRepository<TRepository, TEntity, TId>() 
        where TRepository : notnull, IRepository<TEntity, TId>
        where TEntity : EntityRoot<TId>, new();
    /// <summary>
    /// Metodo para indicar que los cambios han sido completados
    /// </summary>
    public Task CompleteAsync();
}