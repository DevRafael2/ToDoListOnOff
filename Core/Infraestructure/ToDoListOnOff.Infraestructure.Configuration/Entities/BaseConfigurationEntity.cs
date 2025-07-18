using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoListOnOff.Domain.Primitives;

namespace ToDoListOnOff.Infraestructure.Configuration.Entities;

/// <summary>
/// Clase base para implementar configuraciónes genericas
/// en las tablas de la base de datos
/// </summary>
/// <typeparam name="TEntity">Entidad a configurar</typeparam>
/// <typeparam name="TId">Tipo de Id</typeparam>
public abstract class BaseConfigurationEntity<TEntity, TId> where TEntity : EntityRoot<TId>
{
    /// <summary>
    /// Esquema de usuarios
    /// </summary>
    public const string? UsersSchema = "Users";
    
    /// <summary>
    /// Esquema de usuarios
    /// </summary>
    public const string? ToDoListSchema = "ToDoList";

    /// <summary>
    /// Metodo que implementa configuraciones basicas
    /// </summary>
    /// <param name="builder">Model Builder</param>
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        List<Type> numericIds = [typeof(byte), typeof(short), typeof(int), typeof(long)];
        builder.HasKey(x => x.Id);
        if(numericIds.Contains(typeof(TId)))
            builder.Property(x => x.Id).UseIdentityColumn();
            
        builder.HasQueryFilter(e => e.IsDeleted == false);

        builder.Property(e => e.Id).HasComment("Id del registro");
        builder.Property(e => e.IsDeleted).HasComment("Indica si el registro ha sido eliminado");
    }
}