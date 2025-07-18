using MediatR;

namespace ToDoListOnOff.Domain.Interfaces.Validators;

/// <summary>
/// Interfaz de validaciones para cualquier request
/// </summary>
/// <typeparam name="TRequest">Request de mediatR</typeparam>
/// <typeparam name="TResponse">Respuesta a entregar</typeparam>
public interface IValidatorRequest<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Metodo que valida un request
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns>Retorna la respuesta indicada con o sin errores</returns>
    public Task<TResponse> Validate(TRequest request);
}