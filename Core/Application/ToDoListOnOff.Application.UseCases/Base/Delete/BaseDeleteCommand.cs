using MediatR;
using ToDoListOnOff.Domain.Interfaces.Repositories;
using ToDoListOnOff.Domain.Primitives;

namespace ToDoListOnOff.Application.UseCases.Base.Delete;

/// <summary>
/// Base request de comando de eliminación
/// </summary>
/// <param name="id">Id de la entidad</param>
/// <typeparam name="TId">Tipo del id</typeparam>
public class BaseDeleteRequest<TId>(TId id) : IRequest<Response> 
{
    /// <summary>
    /// Id de la entidad
    /// </summary>
    public TId Id { get; } = id;
}

/// <summary>
/// Comando base de eliminación de entidades
/// </summary>
/// <typeparam name="TEntity">Tipo de la entidad</typeparam>
/// <typeparam name="TId">Tipo del id de la entidad</typeparam>
public abstract class BaseDeleteCommand<TEntity, TId>(
        IUnitOfWork unitOfWork
    ): IRequestHandler<BaseDeleteRequest<TId>, Response>
    where TEntity : EntityRoot<TId>, new()
{
    public async Task<Response> Handle(BaseDeleteRequest<TId> request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<TEntity, TId>();
        await repository.DeleteAsync(request.Id);
        await unitOfWork.CompleteAsync();

        return new()
        {
            StatusResponse = StatusResponse.Ok,
            Message = "Registro eliminado correctamente"
        };
    }
}