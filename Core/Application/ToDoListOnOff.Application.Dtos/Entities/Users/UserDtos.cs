namespace ToDoListOnOff.Application.Dtos.Entities.Users;

/// <summary>
/// Dto de entrada para inicio de sesión 
/// </summary>
public class InUserSignIn
{
    /// <summary>
    /// Correo del usuario
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// Contraseña del usuario
    /// </summary>
    public string? Password { get; set; }
}

/// <summary>
/// Dto de salida para inicio de sesión
/// </summary>
public class OutUserSignIn
{
    /// <summary>
    /// Correo del usuario
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// Token de autenticación
    /// </summary>
    public string? Token { get; set; }
}