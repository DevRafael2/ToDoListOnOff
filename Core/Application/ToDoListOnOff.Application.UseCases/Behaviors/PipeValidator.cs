using MediatR;
using ToDoListOnOff.Domain.Interfaces.Validators;
using ToDoListOnOff.Domain.Primitives;

namespace ToDoListOnOff.Application.UseCases.Behaviors;

/// <summary>
/// Pipeline para validaciones
/// </summary>
/// <param name="validator">Validador de request</param>
/// <typeparam name="TRequest">Tipo de request</typeparam>
/// <typeparam name="TResponse">Tipo de respuesta</typeparam>
public class PipeValidator<TRequest, TResponse>(IValidatorRequest<TRequest, TResponse> validator) : 
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Response
{
    /// <inheritDoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validateResponse = await validator.Validate(request);
        if (validateResponse.StatusResponse != StatusResponse.Ok)
            return validateResponse;
        
        var response = await next(cancellationToken);
        return response;
    }
}