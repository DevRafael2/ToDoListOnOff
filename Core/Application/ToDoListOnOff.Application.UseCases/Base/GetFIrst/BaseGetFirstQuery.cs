using MapsterMapper;
using MediatR;
using ToDoListOnOff.Domain.Interfaces.Repositories;
using ToDoListOnOff.Domain.Primitives;

namespace ToDoListOnOff.Application.UseCases.Base.GetFIrst;

/// <summary>
/// Request para consultar un solo registro
/// </summary>
/// <param name="id">Id del registro a consultar</param>
/// <typeparam name="TId">Tipo del id</typeparam>
/// <typeparam name="TOutDto">Tipo del dto de salida</typeparam>
public class BaseGetFirstRequest<TId, TOutDto>(TId id) : IRequest<ResponseData<TOutDto>>
{
    /// <summary>
    /// Id de la entidad a consultar
    /// </summary>
    public TId Id { get; } = id;
}

/// <summary>
/// Query de obtención de un solo registro
/// </summary>
/// <param name="unitOfWork">Unidad de trabajo</param>
/// <param name="mapper">Mapster</param>
/// <typeparam name="TEntity">Tipo de entidad</typeparam>
/// <typeparam name="TId">Tipo de id de la entidad</typeparam>
/// <typeparam name="TRequest">Tipo del request</typeparam>
/// <typeparam name="TOutDto">Tipo de dto de salida</typeparam>
public class BaseGetFirstQuery<TEntity, TId, TRequest, TOutDto>(
        IUnitOfWork unitOfWork,
        IMapper mapper) : 
    IRequestHandler<TRequest, ResponseData<TOutDto>>
    where TEntity : EntityRoot<TId>, new()
    where TRequest : BaseGetFirstRequest<TId, TOutDto>, IRequest<ResponseData<TOutDto>>
{
    
    /// <summary>
    /// Handler para obtener un solo registro
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns>Retorna una respuesta con la entidad solicitada</returns>
    public async Task<ResponseData<TOutDto>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.GetRepository<TEntity, TId>()
            .GetFirstAsync(request.Id);

        if (entity is null)
            return new() { StatusResponse = StatusResponse.NotFound, 
                Message = "No se encontro el registro especificado" };
        
        var entityMap = mapper.Map<TOutDto>(entity);
        return new()
        {
            StatusResponse = StatusResponse.Ok,
            Message = "Se obtiene el registro correctamente",
            Data = entityMap
        };
    }
}