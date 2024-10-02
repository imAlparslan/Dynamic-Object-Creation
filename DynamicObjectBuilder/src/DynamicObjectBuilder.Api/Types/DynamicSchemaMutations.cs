using DynamicObjectBuilder.Business.Exceptions;
using DynamicObjectBuilder.Business.Services;
using DynamicObjectBuilder.Contracts.Common;
using DynamicObjectBuilder.Contracts.DynamicEntityRequests.CreateEntity;
using DynamicObjectBuilder.Contracts.SchemaBuilderRequests.CreateSchema;
using DynamicObjectBuilder.Contracts.SchemaBuilderRequests.DeleteSchema;

namespace DynamicObjectBuilder.Api.Types;

[MutationType]
public static class DynamicSchemaMutations
{
    [Error<SchemaError>]
    public static async Task<DynamicSchemaResponse> CreateSchemaAsync([Service] ISchemaBuilderService schemaBuilderService,
                                             CreateSchemaRequest request,
                                             CancellationToken cancellationToken)
    {

        return await schemaBuilderService.CreateSchemaAsync(request, cancellationToken);
    }

    [Error<SchemaError>]
    public static async Task<bool> DeleteSchemaById([Service] ISchemaBuilderService schemaBuilderService,
                                                    DeleteSchemaById request,
                                                    CancellationToken cancellationToken)
    {
        return await schemaBuilderService.DeleteByIdAsync(request.Id, cancellationToken);
    }

    [Error<SchemaError>]
    public static async Task<Guid> CreateEntityAsync([Service] IEntityService entityService,
                                                     CreateDynamicEntityRequest request,
                                                     CancellationToken cancellationToken)
    {
       return await entityService.CreateEntityAsync(request, cancellationToken);
    }

}