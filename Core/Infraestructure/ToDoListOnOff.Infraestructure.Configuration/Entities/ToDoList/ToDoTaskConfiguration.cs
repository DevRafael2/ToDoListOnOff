using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoListOnOff.Domain.Entities.ToDoList;

namespace ToDoListOnOff.Infraestructure.Configuration.Entities.ToDoList;

/// <summary>
/// Configuración de entidad de tareas
/// </summary>
public class ToDoTaskConfiguration : BaseConfigurationEntity<ToDoTask, int>, IEntityTypeConfiguration<ToDoTask>
{
    /// <inheritDoc />
    public override void Configure(EntityTypeBuilder<ToDoTask> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("ToDoTasks", ToDoListSchema);
        
        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(50)
            .HasComment("Indica el titulo de la tarea");

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(550)
            .HasComment("Indica la descripción de la tarea");
        
        builder.Property(e => e.IsDone)
            .IsRequired()
            .HasDefaultValue(false)
            .HasComment("Indica si la tarea esta completada (true) o esta pendiente (false)");

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasComment("Id del usuario creador y dueño de la tarea");
        
        builder.HasOne(e => e.User).WithMany(e => e.ToDoTasks)
            .HasForeignKey(e => e.UserId)
            .HasPrincipalKey(e => e.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}