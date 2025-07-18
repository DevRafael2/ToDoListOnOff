using System.Linq.Expressions;
using ToDoListOnOff.Domain.Primitives;

namespace ToDoListOnOff.Domain.Interfaces.Repositories;

/// <summary>
/// Interfaz (regla de dominio) para repositorio
/// </summary>
/// <typeparam name="TEntity">Entidad</typeparam>
/// <typeparam name="TId">Tipo de id de la entidad</typeparam>
public interface IRepository<TEntity, TId>
    where TEntity : EntityRoot<TId>, new()
{
            /// <summary>
        /// Metodo para obtener entidades
        /// </summary>
        /// <param name="where">Expresión que funciona para filtrar información</param>
        /// <param name="select">Expresión que funciona para indicar datos a seleccionar</param>
        /// <param name="actualPage">Pagina actual</param>
        /// <param name="pageSize">Tamaño de la pagina</param>
        public Task<ResponseDataPaginate<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TEntity>> select,
            int actualPage, int pageSize);


        /// <summary>
        /// Metodo para obtener entidades
        /// </summary>
        /// <param name="where">Expresión que funciona para filtrar información</param>
        /// <param name="select">Expresión que funciona para indicar datos a seleccionar</param>
        public Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TEntity>> select);


        /// <summary>
        /// Metodo para obtener una entidad
        /// </summary>
        /// <param name="id">Id de la entidad</param>
        public Task<TEntity?> GetFirstAsync(TId id);
        /// <summary>
        /// Metodo para obtener una entidad
        /// </summary>
        /// <param name="id">Id de la entidad</param>
        /// <param name="select">Expresion para traer propiedades necesarias</param>
        public Task<TEntity?> GetFirstAsync(TId id, Expression<Func<TEntity, TEntity>> select);
        /// <summary>
        /// Metodo para obtener una entidad
        /// </summary>
        /// <param name="firstOrDefault">Expresión para buscar la entidad</param>
        /// <param name="select">Expresion para obtener los campos requeridos de la entidad</param>
        public Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> firstOrDefault, Expression<Func<TEntity, TEntity>> select);


        /// <summary>
        /// Metodo para crear una entidad
        /// </summary>
        /// <param name="entity">Entidad que se va a crear</param>
        /// <returns>Se retorna entidad puesto que esta entrega un Id</returns>
        public Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Metodo para actualizar una entidad
        /// </summary>
        /// <param name="entity">Entidad</param>
        public Task UpdateAsync(TEntity entity);
        /// <summary>
        /// Metodo para eliminar una entidad
        /// </summary>
        /// <param name="id">Id de la entidad</param>
        public Task DeleteAsync(TId id);
        /// <summary>
        /// Metodo para remover una entidad (POCO USO)
        /// </summary>
        /// <param name="entity">Entidad a eliminar</param>
        public Task RemoveAsync(TEntity entity);
        
        /// <summary>
        /// Metodo para guardar los cambios 
        /// </summary>
        /// <returns></returns>
        public Task SaveChangesAsync();
}