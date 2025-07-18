using ToDoListOnOff.Domain.Entities.Users;
using ToDoListOnOff.Domain.Primitives;

namespace ToDoListOnOff.Domain.Entities.ToDoList;

/// <summary>
/// Entidad que modela las tareas
/// </summary>
public class ToDoTask : EntityRoot<int>
{
    /// <summary>
    /// Titulo
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// Descripcion de la tarea
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Indica si se completo la tarea (true) o esta pendiente (false)
    /// </summary>
    public bool? IsDone { get; set; }
    
    /// <summary>
    /// Id del usuario
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Usuario, dueño de la tarea
    /// </summary>
    public virtual User? User { get; set; }
}