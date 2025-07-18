using System.Linq.Expressions;
using MediatR;

namespace ToDoListOnOff.Domain.Primitives;

/// <summary>
/// Clase abstracta basica con propiedades simples de busqueda y paginado
/// </summary>
public abstract class BaseQueryParams
{
    /// <summary>
    /// Pagina actual
    /// </summary>
    public int CurrentPage { get; set; } = 1;

    /// <summary>
    /// Tamaño de la pagina
    /// </summary>
    public int PageSize { get; set; } = 10;
}

/// <summary>
/// Clase compuesta querparams para busquedas
/// </summary>
/// <typeparam name="TEntity">Entidad</typeparam>
/// <typeparam name="TOutDto">Dto de salida para marcar como IRequest</typeparam>
public abstract class QueryParams<TEntity, TOutDto> : BaseQueryParams, IRequest<ResponseDataPaginate<TOutDto>>
{
    /// <summary>
    /// Metodo para obtener expression Where (Filtros)
    /// </summary>
    public abstract Expression<Func<TEntity, bool>> GetWhereExpression();
    /// <summary>
    /// Metodo para obtener expression Select (Consulta)
    /// </summary>
    public abstract Expression<Func<TEntity, TEntity>> GetSelectExpression();
}