using ToDoListOnOff.Application.Dtos.Entities.ToDoList;
using ToDoListOnOff.Application.UseCases.Base.Update;
using ToDoListOnOff.Domain.Interfaces.Validators;
using ToDoListOnOff.Domain.Primitives;

namespace ToDoListOnOff.Application.Validators.Entities.ToDoList.ToDoTask;

/// <summary>
/// Validador para comando de actualización de tareas
/// </summary>
public class UpdateToDoTaskValidator : IValidatorRequest<BaseUpdateRequest<InToDoTask, int>,  Response>
{
    /// <inheritDoc />
    public async Task<Response> Validate(BaseUpdateRequest<InToDoTask, int> request)
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