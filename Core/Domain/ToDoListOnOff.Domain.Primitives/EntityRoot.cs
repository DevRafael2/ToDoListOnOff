namespace ToDoListOnOff.Domain.Primitives;

/// <summary>
/// Entidad base, no se manejarán eventos,
/// se exlucye event sourcing
/// </summary>
public class EntityRoot<TId>
{
    /// <summary>
    /// Id de la entidad
    /// </summary>
    public TId? Id { get; set; }
    /// <summary>
    /// Indica si la entidad ha sido eliminada
    /// </summary>
    public bool IsDeleted { get; set; }
}