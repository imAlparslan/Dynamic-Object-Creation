using DynamicObjectBuilder.Business.Services;
using DynamicObjectBuilder.DataAccess.Models.DynamicEntityModels;

namespace DynamicObjectBuilder.Api.Types;

[QueryType]
public static class DynamicEntityQuery
{

    public static async Task<DynamicEntity> GetDynamicEntityByEntityIdAsync([Service] IDynamicEntityService dynamicEntityService, Guid entityId, CancellationToken cancellationToken)
    {
        return await dynamicEntityService.GetDynamicEntityByEntityIdAsync(entityId, cancellationToken);
    }

    public static async Task<IEnumerable<DynamicEntity>> GetDynamicEntityBySchemaIdAsync([Service] IDynamicEntityService dynamicEntityService, Guid schemaId, CancellationToken cancellationToken)
    {
        return await dynamicEntityService.GetDynamicEntityBySchemaIdAsync(schemaId, cancellationToken);
    }
}
