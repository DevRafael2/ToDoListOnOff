using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoListOnOff.Application.Dtos.Entities.Users;
using ToDoListOnOff.Application.UseCases.Entities.Users.Commands;
using ToDoListOnOff.Domain.Primitives;

namespace TodoListOnOff.Api.Controllers.V1;

/// <summary>
/// Controlador de usuarios
/// </summary>
/// <param name="sender">Sender de mediatR</param>
[ApiController, Route("api/v1/users")]
public class UserController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Endpoint para iniciar sesión
    /// </summary>
    /// <param name="credentials">Credenciales del usuario</param>
    [HttpPost("sign-in")]
    public async Task<ActionResult<ResponseData<OutUserSignIn>>> SignIn(InUserSignIn credentials)
    {
        var result = await sender.Send(new SignInRequest(credentials));
        return StatusCode(result.StatusResponse.GetHashCode(), result);
    }
}