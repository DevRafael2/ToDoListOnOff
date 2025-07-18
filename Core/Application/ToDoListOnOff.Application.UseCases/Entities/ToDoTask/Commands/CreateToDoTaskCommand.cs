namespace ToDoListOnOff.Application.UseCases.Entities.ToDoTask.Commands;

using Domain.Primitives;
using MapsterMapper;
using Domain.Interfaces.Repositories;
using Dtos.Entities.ToDoList;
using Base.Create;
using Domain.Entities.ToDoList;

/// <summary>
/// Request para creación de tareas
/// </summary>
/// <param name="entity">Entidad a crear</param>
public class CreateToDoTaskRequest(InToDoTask entity, Guid userId) : BaseCreateRequest<InToDoTask, OutToDoTask>(entity)
{
    /// <summary>
    /// Id del usuario
    /// </summary>
    public Guid UserId { get; } = userId;
}

/// <summary>
/// Comando de creación para tareas
/// </summary>
/// <param name="unitOfWork">Unidad de trabajo</param>
/// <param name="mapper">Mapper</param>
public class CreateToDoTaskCommand(IUnitOfWork unitOfWork, IMapper mapper) : 
    BaseCreateCommand<ToDoTask, int, InToDoTask, OutToDoTask, CreateToDoTaskRequest>(unitOfWork, mapper)
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
    public override async Task<ResponseData<OutToDoTask>> Handle(CreateToDoTaskRequest request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<ToDoTask, int>();
        
        var createEntity = _mapper.Map<ToDoTask>(request.Entity);
        createEntity.UserId = request.UserId;
        
        var entityCreate = await repository.CreateAsync(createEntity);
        await repository.SaveChangesAsync();
        var entityOutCreate = _mapper.Map<OutToDoTask>(entityCreate);
        return new () {
            Data = entityOutCreate,
            StatusResponse = StatusResponse.Ok,
            Message = "Registro creado correctamente",
        };
    }
}