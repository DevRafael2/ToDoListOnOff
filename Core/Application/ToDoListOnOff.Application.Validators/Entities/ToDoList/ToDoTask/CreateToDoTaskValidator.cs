namespace ToDoListOnOff.Application.Validators.Entities.ToDoList.ToDoTask;

using Dtos.Entities.ToDoList;
using UseCases.Entities.ToDoTask.Commands;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Validators;
using Domain.Primitives;
using Domain.Entities.ToDoList;

/// <summary>
/// Validado para comando de creacion de tareas
/// </summary>
/// <param name="repository">Repositorio</param>
public class CreateToDoTaskValidator(IRepository<ToDoTask, int> repository) : 
    IValidatorRequest<CreateToDoTaskRequest, ResponseData<OutToDoTask>>
{
    /// <inheritDoc />
    public async Task<ResponseData<OutToDoTask>> Validate(CreateToDoTaskRequest request)
    {
        var dto = request.Entity;
        var responseWithErrors = new ResponseData<OutToDoTask> {
            Message = "La información contiene errores",
            StatusResponse = StatusResponse.BadRequest
        };

        if (dto.Title is null or { Length: < 2 or > 50 })
            responseWithErrors.Errors.Add("El título debe tener entre 2 a 50 caracteres");
        if (dto.Description is null or { Length: < 2 or > 550 })
            responseWithErrors.Errors.Add("La descripción debe tener entre 2 a 550 caracteres");
        
        if(responseWithErrors.Errors.Count > 0)
            return responseWithErrors;

        return new() { StatusResponse = StatusResponse.Ok };
    }
}