using DynamicObjectBuilder.Business.Exceptions;
using DynamicObjectBuilder.Business.Services;
using DynamicObjectBuilder.Contracts.DynamicEntityRequests;
using DynamicObjectBuilder.DataAccess.Models.DynamicEntityModels;
namespace DynamicObjectBuilder.Api.Types;

[MutationType]
public static class DynamicEntityMutations
{
    [Error<DynamicEntityError>]
    public static async Task<DynamicEntity> CreateDynamicEntity([Service] IDynamicEntityService dynamicEntityService,
        CreateDynamicEntityRequest request,
        CancellationToken cancellationToken)
    {
        return await dynamicEntityService.CreateDynamicEntityAsync(request, cancellationToken);
    }

    [Error<DynamicEntityError>]
    public static async Task<bool> DeleteEntityByIdAsync([Service] IDynamicEntityService dynamicEntityService, Guid entityId, CancellationToken cancellationToken)
    {
        return await dynamicEntityService.DeleteEntityById(entityId,cancellationToken);
    }
}