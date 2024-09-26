using DynamicObjectBuilder.Business.Exceptions;
using DynamicObjectBuilder.Business.Services;
using DynamicObjectBuilder.Contracts.SchemaBuilderRequests;
using DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;

namespace DynamicObjectBuilder.Api.Types;

[MutationType]
public static class DynamicSchemaMutations
{
    [Error<SchemaError>]
    public static async Task<DynamicSchema> CreateSchemaAsync([Service] ISchemaBuilderService schemaBuilderService,
                                             CreateSchemaRequest request,
                                             CancellationToken cancellationToken)
    {
        return await schemaBuilderService.CreateSchemaAsync(request, cancellationToken);

    }

    [Error<SchemaError>]
    public static async Task<bool> DeleteSchemaAsync([Service] ISchemaBuilderService schemaBuilderService, Guid schemaId, CancellationToken cancellationToken)
    {
        return await schemaBuilderService.DeleteByIdAsync(schemaId, cancellationToken);
    }

    [Error<SchemaError>]
    public static async Task<DynamicSchema> UpdateSchemaAsync([Service] ISchemaBuilderService schemaBuilderService, UpdateSchemaRequest request, CancellationToken cancellationToken)
    {
        return await schemaBuilderService.UpdateSchemaAsync(request, cancellationToken);
    }
}