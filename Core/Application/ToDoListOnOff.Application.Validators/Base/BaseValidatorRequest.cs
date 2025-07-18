using MediatR;
using ToDoListOnOff.Domain.Interfaces.Validators;
using ToDoListOnOff.Domain.Primitives;

namespace ToDoListOnOff.Application.Validators.Base;

/// <summary>
/// Validador base
/// </summary>
/// <typeparam name="TRequest">Request</typeparam>
/// <typeparam name="TResponse">Tipo de respuesta</typeparam>
public class BaseValidatorRequest<TRequest, TResponse> : IValidatorRequest<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Response, new()
{
    /// <inheritDoc />
    public Task<TResponse> Validate(TRequest request) => 
        Task.FromResult(new TResponse { StatusResponse = StatusResponse.Ok });
}