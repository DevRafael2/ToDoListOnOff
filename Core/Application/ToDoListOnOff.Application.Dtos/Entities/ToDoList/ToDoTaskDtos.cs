namespace ToDoListOnOff.Application.Dtos.Entities.ToDoList;

/// <summary>
/// Dto de entrada para tareas
/// </summary>
public class InToDoTask 
{
    /// <summary>
    /// Titulo de la tarea
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// Descripción de la tarea
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// Dto de salida para tareas
/// </summary>
public class OutToDoTask : InToDoTask
{
    /// <summary>
    /// Id de la tarea
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Indica si la tarea fue completada
    /// </summary>
    public bool IsDone { get; set; }
}