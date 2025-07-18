using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ToDoListOnOff.Application.Dtos.Entities.Users;
using ToDoListOnOff.Domain.Entities.Users;
using ToDoListOnOff.Domain.Interfaces.Repositories;
using ToDoListOnOff.Domain.Primitives;
using ToDoListOnOff.Transversal.Helpers.Strings;

namespace ToDoListOnOff.Application.UseCases.Entities.Users.Commands;

/// <summary>
/// Request para inicio de sesión
/// </summary>
/// <param name="Credentials">Credenciales del usuario</param>
public record SignInRequest(InUserSignIn Credentials) : IRequest<ResponseData<OutUserSignIn>>;

/// <summary>
/// Comando para iniciar sesión
/// </summary>
/// <param name="repository">Repositorio para interactuar con los usuarios</param>
public class SignInCommand(
    IConfiguration configuration,
    IRepository<User, Guid> repository) : IRequestHandler<SignInRequest, ResponseData<OutUserSignIn>>
{
    public async Task<ResponseData<OutUserSignIn>> Handle(SignInRequest request, CancellationToken cancellationToken)
    {
        var userExist = await repository
            .GetFirstAsync(
                firstOrDefault: e => e.Email!.ToLower() == request.Credentials.Email!.ToLower() &&
                                    e.Password == Sha256Helper.ComputeSha256Hash(request.Credentials.Password!),
                select: e => new() { Id = e.Id });
        
        if (userExist is null)
            return new ResponseData<OutUserSignIn>() { Message = "Credenciales incorrectas", StatusResponse = StatusResponse.UnAuthorize };

        var token = CreateToken(userExist.Id, request.Credentials.Email!);

        return new() { 
            StatusResponse = StatusResponse.Ok, 
            Message = "Usuario autenticado correctamente", 
            Data = new()
            {
                Token = token,
                Email = request.Credentials.Email!,
            }
        };
    }

    /// <summary>
    /// Metodo para generar un token de autenticación
    /// </summary>
    /// <param name="userId">Id del usuario</param>
    /// <param name="email">Correo del usuario</param>
    /// <returns>Retorna token del usuario</returns>
    private string CreateToken(Guid userId, string email)
    {
        var createdAt = DateTime.UtcNow.ToString("dd-MM-yyyy");
        var claims = new List<Claim> {
            new ("email", email),
            new ("userId", userId.ToString()),
            new ("createdAt", createdAt),
        };

        var keySymmetric = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyTokenSecurity"]!));
        var creds = new SigningCredentials(keySymmetric, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: DateTime.Now.AddDays(10),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}