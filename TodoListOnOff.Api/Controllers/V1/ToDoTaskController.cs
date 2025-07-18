using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListOnOff.Api.Base.Controllers;
using ToDoListOnOff.Application.Dtos.Entities.ToDoList;
using ToDoListOnOff.Application.QueryParams.Entities.ToDoList;
using ToDoListOnOff.Application.UseCases.Base.Update;
using ToDoListOnOff.Application.UseCases.Entities.ToDoTask.Commands;
using ToDoListOnOff.Application.UseCases.Entities.ToDoTask.Queries;
using ToDoListOnOff.Domain.Entities.ToDoList;
using ToDoListOnOff.Domain.Primitives;

namespace TodoListOnOff.Api.Controllers.V1;

/// <summary>
/// Controlador para tareas
/// </summary>
/// <param name="sender">Sender de mediador</param>
[ApiController, Route("api/v1/to-do-task"), Authorize(AuthenticationSchemes = "Bearer")]
public class ToDoTaskController(ISender sender) : 
    BaseController<
        ToDoTask,
        int,
        OutToDoTask,
        OutToDoTask,
        InToDoTask,
        OutToDoTask,
        InToDoTask,
        ToDoTaskQueryParams,
        ToDoTaskFirstRequest,
        CreateToDoTaskRequest,
        BaseUpdateRequest<InToDoTask, int>,
        DeleteToDoTaskRequest
    >(sender)
{
    /// <inheritDoc />
    public override async Task<ActionResult<ResponseData<OutToDoTask>>> CreateAsync([FromBody]InToDoTask entity)
    {
        var userIdClaim = HttpContext.User.FindFirst(e => e.Type == "userId");
        if (userIdClaim is null)
            return StatusCode(403, new Response 
                { StatusResponse = StatusResponse.Forbbiden, Message = "Usuario no autenticado correctamente" });
        
        var result = await Sender.Send(new CreateToDoTaskRequest(entity, Guid.Parse(userIdClaim.Value)));
        return StatusCode(result.StatusResponse.GetHashCode(), result);
    }

    /// <summary>
    /// Metodo para actualizar el estado de una tarea 
    /// </summary>
    /// <param name="taskId">Id de la tarea</param>
    /// <returns>Retorna respuesta con indicador de exito</returns>
    [HttpPatch("{taskId}")]
    public async Task<ActionResult<Response>> ChangeState([FromRoute]int taskId)
    {
        var result = await Sender.Send(new UpdateTaskStateRequest(taskId));
        return StatusCode(result.StatusResponse.GetHashCode(), result);
    }
}