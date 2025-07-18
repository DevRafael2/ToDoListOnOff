
using MediatR;
using ToDoListOnOff.Application.Dtos.Entities.ToDoList;
using ToDoListOnOff.Domain.Primitives;

namespace ToDoListOnOff.Application.UseCases.Entities.ToDoTask.Commands;

using MapsterMapper;
using Domain.Interfaces.Repositories;
using Base.Update;
using Domain.Entities.ToDoList;

/// <summary>
/// Comando para actualizar tareas
/// </summary>
/// <param name="unitOfWork">Unidad de trabajo</param>
/// <param name="mapper">Mapster</param>
public class UpdateToDoTaskCommand(IUnitOfWork unitOfWork, IMapper mapper) :
    BaseUpdateCommand<ToDoTask, InToDoTask, int>(unitOfWork, mapper)
{
    /// <summary>
    /// Unidad de trabajo
    /// </summary>
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    /// <summary>
    /// Mapster
    /// </summary>
    private readonly IMapper _mapper = mapper;

    /// <inheritDoc />
    public override async Task<Response> Handle(BaseUpdateRequest<InToDoTask, int> request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<ToDoTask, int>();
        var entity = await repository.GetFirstAsync(request.Id, e => new() { UserId = e.UserId });
        if (entity is null)
            return new() { StatusResponse = StatusResponse.NotFound, Message = "No se encontro la entidad" };
        
        var updateEntity = _mapper.Map<ToDoTask>(request.Entity);
        updateEntity.UserId = entity.UserId;
        
        await repository.UpdateAsync(updateEntity);
        await repository.SaveChangesAsync();
        return new Response {
            StatusResponse = StatusResponse.Ok,
            Message = "Registro actualizado correctamente",
        };
    }

}