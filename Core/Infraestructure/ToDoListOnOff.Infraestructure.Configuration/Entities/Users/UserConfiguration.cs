using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoListOnOff.Domain.Entities.Users;

namespace ToDoListOnOff.Infraestructure.Configuration.Entities.Users;

/// <summary>
/// Configuración de entidad usuarios
/// </summary>
public class UserConfiguration : BaseConfigurationEntity<User, Guid>, IEntityTypeConfiguration<User>
{
    /// <inheritDoc />
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.ToTable("Users", UsersSchema);
        
        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired()
            .HasComment("Nombre del usuario");

        builder.Property(e => e.Email)
            .HasMaxLength(150)
            .IsRequired()
            .HasComment("Correo del usuario");

        builder.Property(e => e.Password)
            .HasMaxLength(-1)
            .IsRequired()
            .HasComment("Contraseña encriptada del usuario SHA256");

        builder.HasData([
            new() { Id =  Guid.Parse("c9e89d99-6a2b-4c9d-8e3d-bc1253d4aadb"), Name = "Test Prueba", Email = "test@test.com", 
                Password = "601e04837eb0743d97e69e5ed54129ab45268f9ce03b179e14a523cf9fc1edb1" } // TestUser.1
        ]);
    }
}