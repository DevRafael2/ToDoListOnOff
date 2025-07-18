using ToDoListOnOff.Domain.Entities.ToDoList;
using ToDoListOnOff.Domain.Primitives;

namespace ToDoListOnOff.Domain.Entities.Users;

/// <summary>
/// Entidad de usuario
/// </summary>
public class User : EntityRoot<Guid>
{
    /// <summary>
    /// Nombre del usuario
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Correo del usuario
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// Contraseña del usuario
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Tareas realcionadas con el usuario
    /// </summary>
    public virtual ICollection<ToDoTask>? ToDoTasks { get; set; }
}