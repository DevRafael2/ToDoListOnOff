namespace ToDoListOnOff.Application.UseCases.Entities.ToDoTask.Commands;

using Domain.Interfaces.Repositories;
using Base.Delete;
using Domain.Entities.ToDoList;

/// <summary>
/// Request para eliminación de tareas
/// </summary>
/// <param name="id">Id de la tarea</param>
public class DeleteToDoTaskRequest(int id) : BaseDeleteRequest<int>(id);

/// <summary>
/// Comando de eliminación para tareas
/// </summary>
/// <param name="unitOfWork">Unidad de trabajo</param>
public class DeleteToDoTaskCommand(IUnitOfWork unitOfWork) : BaseDeleteCommand<ToDoTask, int>(unitOfWork);