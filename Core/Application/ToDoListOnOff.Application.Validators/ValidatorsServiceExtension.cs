using Microsoft.Extensions.DependencyInjection;
using ToDoListOnOff.Application.Dtos.Entities.ToDoList;
using ToDoListOnOff.Application.UseCases.Base.Update;
using ToDoListOnOff.Application.UseCases.Entities.ToDoTask.Commands;
using ToDoListOnOff.Application.Validators.Base;
using ToDoListOnOff.Application.Validators.Entities.ToDoList.ToDoTask;
using ToDoListOnOff.Domain.Interfaces.Validators;
using ToDoListOnOff.Domain.Primitives;

namespace ToDoListOnOff.Application.Validators;

/// <summary>
/// Extension para agregar validadores al service collection
/// </summary>
public static class ValidatorsServiceExtension
{
    /// <summary>
    /// Metodo extensivo para agregar validadores al service collection
    /// </summary>
    /// <param name="services">Contenedor de dependencias</param>
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped(typeof(IValidatorRequest<,>), typeof(BaseValidatorRequest<,>));
        
        services.AddScoped<IValidatorRequest<CreateToDoTaskRequest, ResponseData<OutToDoTask>>, CreateToDoTaskValidator>();
        services.AddScoped<IValidatorRequest<BaseUpdateRequest<InToDoTask, int>, Response>, UpdateToDoTaskValidator>();
        return services;
    }
}