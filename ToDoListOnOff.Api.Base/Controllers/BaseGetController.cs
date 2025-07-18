using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoListOnOff.Domain.Primitives;

namespace ToDoListOnOff.Api.Base.Controllers;

/// <summary>
/// Controlador base para metodos de obtención
/// </summary>
/// <param name="sender">Sender de mediatR</param>
public class BaseGetController<
    TEntity,
    TId,
    TGetDto,
    TGetFirstDto,
    TQueryGet,
    TQueryGetFirst
>(ISender sender) : ControllerBase
    where TQueryGet : QueryParams<TEntity, TGetDto>
    where TQueryGetFirst : IRequest<ResponseData<TGetFirstDto>>
    where TEntity : EntityRoot<TId>
{
    /// <summary>
    /// Sender de mediatR
    /// </summary>
    protected ISender Sender { get; } = sender;

    /// <summary>
    /// Metodo para obtener datos paginados
    /// </summary>
    /// <param name="query">QueryParams</param>
    /// <returns>Retorna una respuesta paginada con datos de la entidad</returns>
    [HttpGet]
    public virtual async Task<ActionResult<ResponseDataPaginate<TGetDto>>> GetAsync([FromQuery]TQueryGet query)
    {
        var result = await Sender.Send(query!);
        return StatusCode(result.StatusResponse.GetHashCode(), result);
    }

    /// <summary>
    /// Metodo para obtener un solo registro por Id
    /// </summary>
    /// <param name="queryId">Request con Id del registro</param>
    /// <returns>Retorna respuesta con entidad</returns>
    [HttpGet("{queryId}")]
    public virtual async Task<ActionResult<ResponseData<TGetFirstDto>>> GetFirstAsync([FromRoute]TQueryGetFirst queryId)
    {
        var result = await Sender.Send(queryId);
        return StatusCode(result.StatusResponse.GetHashCode(), result);
    }
}