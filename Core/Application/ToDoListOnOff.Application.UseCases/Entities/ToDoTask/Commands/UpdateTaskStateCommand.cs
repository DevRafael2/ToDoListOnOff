namespace ToDoListOnOff.Application.UseCases.Entities.ToDoTask.Commands;

using Domain.Interfaces.Repositories;
using MediatR;
using Domain.Primitives;
using Domain.Entities.ToDoList;

/// <summary>
/// Id de la tarea 
/// </summary>
/// <param name="TaskId">Id de la tarea</param>
public record UpdateTaskStateRequest(int TaskId) : IRequest<Response>; 

/// <summary>
/// Comando para actualizar el estado de una tarea
/// </summary>
/// <param name="repository">Repositorio</param>
public class UpdateTaskStateCommand(IRepository<ToDoTask, int> repository) : IRequestHandler<UpdateTaskStateRequest, Response>
{
    /// <summary>
    /// Handler para actualizar estado de una tarea
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Retorna respuesta</returns>
    public async Task<Response> Handle(UpdateTaskStateRequest request, CancellationToken cancellationToken)
    {
        var taskExist = await repository.GetFirstAsync(request.TaskId);
        if (taskExist is null)
            return new() { StatusResponse = StatusResponse.NotFound, Message = "Tarea no encontrada" };

        taskExist.IsDone = !taskExist.IsDone;
        await repository.UpdateAsync(taskExist);
        await repository.SaveChangesAsync();
        
        return new() { StatusResponse = StatusResponse.Ok, Message = $"Estado de la tarea cambiado a {taskExist.IsDone}" };
    }
}