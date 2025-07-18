namespace ToDoListOnOff.Application.UseCases.Entities.ToDoTask.Queries;

using MapsterMapper;
using Domain.Interfaces.Repositories;
using Dtos.Entities.ToDoList;
using Base.GetFIrst;
using Domain.Entities.ToDoList;

/// <summary>
/// Request para obtener un solo registro
/// </summary>
/// <param name="id">Id de la tarea</param>
public class ToDoTaskFirstRequest(int id) : BaseGetFirstRequest<int, OutToDoTask>(id);

/// <summary>
/// Handler para obtener una sola tarea
/// </summary>
/// <param name="unitOfWork">Unidad de trabajo</param>
/// <param name="mapper">Mapster</param>
public class ToDoTaskFirstQuery(IUnitOfWork unitOfWork, IMapper mapper) : 
    BaseGetFirstQuery<ToDoTask, int, ToDoTaskFirstRequest, OutToDoTask>(unitOfWork, mapper);