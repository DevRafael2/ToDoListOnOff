using MapsterMapper;
using MediatR;
using ToDoListOnOff.Domain.Interfaces.Repositories;
using ToDoListOnOff.Domain.Primitives;

namespace ToDoListOnOff.Application.UseCases.Base.Create;

/// <summary>
/// Request base para comando de creación de entidades
/// </summary>
/// <param name="entity">Entidad (dto)</param>
/// <typeparam name="TInDto">Tipo del dto</typeparam>
/// <typeparam name="TOutDto">Tipo del dto de salida</typeparam>
public class BaseCreateRequest<TInDto, TOutDto>(TInDto entity)
    : IRequest<ResponseData<TOutDto>>
{
    /// <summary>
    /// Entidad
    /// </summary>
    public TInDto Entity { get; } = entity;
}

/// <summary>
/// Comando de creación para entidades
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TId"></typeparam>
/// <typeparam name="TInDto"></typeparam>
/// <typeparam name="TOutDto"></typeparam>
public abstract class BaseCreateCommand<TEntity, TId, TInDto, TOutDto, TRequest>(
    IUnitOfWork unitOfWork,
    IMapper mapper) : 
    IRequestHandler<TRequest, ResponseData<TOutDto>>
    where  TEntity : EntityRoot<TId>, new()
    where TRequest : BaseCreateRequest<TInDto, TOutDto>
{
    /// <summary>
    /// Handler para crear entidades
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public virtual async Task<ResponseData<TOutDto>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<TEntity, TId>();
        if (request.Entity is null)
            return new() { StatusResponse = StatusResponse.BadRequest, Errors = ["No proporciono información"] };
        
        var createEntity = mapper.Map<TEntity>(request.Entity);
        
        var entityCreate = await repository.CreateAsync(createEntity);
        await repository.SaveChangesAsync();
        var entityOutCreate = mapper.Map<TOutDto>(entityCreate);
        return new () {
            Data = entityOutCreate,
            StatusResponse = StatusResponse.Ok,
            Message = "Registro creado correctamente",
        };
    }
}