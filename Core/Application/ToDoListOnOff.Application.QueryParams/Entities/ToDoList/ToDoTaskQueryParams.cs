using System.Linq.Expressions;
using ToDoListOnOff.Application.Dtos.Entities.ToDoList;
using ToDoListOnOff.Domain.Entities.ToDoList;
using ToDoListOnOff.Domain.Primitives;

namespace ToDoListOnOff.Application.QueryParams.Entities.ToDoList;

/// <summary>
/// Query params para ToDoTask
/// </summary>
public class ToDoTaskQueryParams : QueryParams<ToDoTask, OutToDoTask>
{
    /// <summary>
    /// Titulo de la tarea
    /// </summary>
    public string? Title { get; set; } = null;
    /// <summary>
    /// Indica si la tarea fue completada
    /// </summary>
    public bool? IsDone { get; set; } = null;
    
    /// <inheritDoc />
    public override Expression<Func<ToDoTask, bool>> GetWhereExpression() => 
        e => (IsDone == null || e.IsDone == IsDone) && 
             (string.IsNullOrEmpty(Title) || e.Title!.ToLower().Contains(Title.ToLower()));

    /// <inheritDoc />
    public override Expression<Func<ToDoTask, ToDoTask>> GetSelectExpression() => e => e;
}