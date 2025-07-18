namespace ToDoListOnOff.Application.UseCases.Entities.ToDoTask.Queries;

using MapsterMapper;
using Dtos.Entities.ToDoList;
using QueryParams.Entities.ToDoList;
using Base.Get;
using Domain.Interfaces.Repositories;
using Domain.Entities.ToDoList;

/// <summary>
/// Query para obtener tareas
/// </summary>
/// <param name="unitOfWork">Unidad de trabajo</param>
/// <param name="mapper">Mapster</param>
public class ToDoTaskGetQuery(IUnitOfWork unitOfWork, IMapper mapper) : 
    BaseGetQuery<ToDoTask, int, ToDoTaskQueryParams, OutToDoTask>(unitOfWork, mapper);