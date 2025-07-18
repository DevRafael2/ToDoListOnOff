using MapsterMapper;
using MediatR;
using ToDoListOnOff.Domain.Interfaces.Repositories;
using ToDoListOnOff.Domain.Primitives;

namespace ToDoListOnOff.Application.UseCases.Base.Update;

/// <summary>
/// Base de request para comandos de actualización
/// </summary>
/// <param name="id">Id de la entidad</param>
/// <param name="entity">Entidad a actualizar</param>
/// <typeparam name="TInDto">Tipo de la entidad</typeparam>
/// <typeparam name="TId">Tipo del id de la entidad</typeparam>
public class BaseUpdateRequest<TInDto, TId>(TId id, TInDto entity)
    : IRequest<Response>
{
    /// <summary>
    /// Id de la entidad
    /// </summary>
    public TId Id { get; } = id;
    /// <summary>
    /// Entidad a actualizar
    /// </summary>
    public TInDto Entity { get; } = entity;
}

/// <summary>
/// Comando base con accion de actualizar entidad
/// </summary>
/// <typeparam name="TEntity">Tipo de la entidad</typeparam>
/// <typeparam name="TInDto">Dto de entrada</typeparam>
/// <typeparam name="TId">Tipo del id de la entidad</typeparam>
public abstract class BaseUpdateCommand<
        TEntity, TInDto, TId
    >(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<BaseUpdateRequest<TInDto, TId>, Response>
    where TEntity : EntityRoot<TId>, new()
{
    /// <summary>
    /// Handler para actualizar entidades
    /// </summary>
    /// <param name="request">Request base del comando</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Retorna una respuesta</returns>
    public virtual async Task<Response> Handle(BaseUpdateRequest<TInDto, TId> request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<TEntity, TId>();
        var updateEntity = mapper.Map<TEntity>(request.Entity!);
        
        await repository.UpdateAsync(updateEntity);
        await repository.SaveChangesAsync();
        return new Response {
            StatusResponse = StatusResponse.Ok,
            Message = "Registro actualizado correctamente",
        };
    }
}