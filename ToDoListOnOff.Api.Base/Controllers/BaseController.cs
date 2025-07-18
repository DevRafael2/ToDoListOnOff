using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoListOnOff.Application.UseCases.Base.Update;
using ToDoListOnOff.Domain.Primitives;

namespace ToDoListOnOff.Api.Base.Controllers;

public class BaseController<
    TEntity,
    TId,
    TGetDto,
    TGetFirstDto,
    TCreateDto,
    TOutCreateDto,
    TUpdateDto,
    TQueryGet,
    TQueryGetFirst,
    TCreateCommand,
    TUpdateCommand,
    TDeleteCommand
>(ISender sender) : BaseGetController<TEntity, TId, TGetDto, TGetFirstDto, TQueryGet, TQueryGetFirst>(sender) 
    where TQueryGet : QueryParams<TEntity, TGetDto>
    where TQueryGetFirst : IRequest<ResponseData<TGetFirstDto>>
    where TEntity : EntityRoot<TId>
    where TUpdateCommand : BaseUpdateRequest<TUpdateDto, TId>
{
    /// <summary>
    /// Endpoint para crear entidad
    /// </summary>
    /// <param name="entity">Entidad dto de entrada</param>
    /// <returns>Retorna una respuesta con el dto del registro creado</returns>
    [HttpPost]
    public virtual async Task<ActionResult<ResponseData<TOutCreateDto>>> CreateAsync([FromBody]TCreateDto entity)
    {
        var result =(ResponseData<TOutCreateDto>)
            (await Sender.Send(Activator.CreateInstance(typeof(TCreateCommand), entity)!))!;
        return StatusCode(result.StatusResponse.GetHashCode(), result);
    }
    
    /// <summary>
    /// Endpoint para actualizar registros
    /// </summary>
    /// <param name="id">Id del registro</param>
    /// <param name="updateDto">Dto de entrada para atualización</param>
    /// <returns>Retorna respuesta</returns>
    [HttpPut("{id}")]
    public virtual async Task<ActionResult<Response>> UpdateAsync([FromRoute]TId id, [FromBody] TUpdateDto updateDto)
    {
        var request = (TUpdateCommand)Activator.CreateInstance(typeof(TUpdateCommand), [id, updateDto])!;
        var result = (Response)(await Sender.Send(request))!;
        return StatusCode(result!.StatusResponse.GetHashCode(), result);
    }

    /// <summary>
    /// Metodo para eliminar registros (SoftDelete)
    /// </summary>
    /// <param name="id">Id del registro a eliminar</param>
    /// <returns>Entrega una respuesta con datos de la operación</returns>
    [HttpDelete("{id}")]
    public virtual async Task<ActionResult<Response>> DeleteAsync([FromRoute]TId id)
    {
        var result = (Response)(await Sender.Send(Activator.CreateInstance(typeof(TDeleteCommand), id)!))!;
        return StatusCode(result.StatusResponse.GetHashCode(), result);
    }
}