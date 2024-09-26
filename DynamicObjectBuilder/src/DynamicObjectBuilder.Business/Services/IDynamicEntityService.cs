using DynamicObjectBuilder.Contracts.DynamicEntityRequests;
using DynamicObjectBuilder.DataAccess.Models.DynamicEntityModels;

namespace DynamicObjectBuilder.Business.Services;
public interface IDynamicEntityService
{
    Task<DynamicEntity> CreateDynamicEntityAsync(CreateDynamicEntityRequest request, CancellationToken cancellationToken);
    Task<DynamicEntity> GetDynamicEntityByEntityIdAsync(Guid entityId, CancellationToken cancellationToken);
    Task<IEnumerable<DynamicEntity>> GetDynamicEntityBySchemaIdAsync(Guid schemaId, CancellationToken cancellationToken);
    Task<bool> DeleteEntityById(Guid entityId, CancellationToken cancellationToken);
}
