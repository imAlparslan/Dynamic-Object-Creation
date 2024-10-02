using DynamicObjectBuilder.Business.Exceptions;
using DynamicObjectBuilder.Business.Services;
using DynamicObjectBuilder.Contracts.Common;
namespace DynamicObjectBuilder.Api.Types;

[QueryType]
public static class DynamicSchemaQueries
{
    [Error<SchemaError>]
    public static async Task<DynamicSchemaResponse> GetSchemaById(
        [Service] ISchemaBuilderService schemaBuilderService,
        Guid Id,
        CancellationToken cancellationToken)
    {
        return await schemaBuilderService.GetByIdAsync(Id, cancellationToken);
    }

    [Error<SchemaError>]
    public static async Task<List<DynamicSchemaResponse>> GetAllSchema(
        [Service] ISchemaBuilderService schemaBuilderService,
        CancellationToken cancellationToken)
    {
        return await schemaBuilderService.GetAllAsync(cancellationToken);
    }
}
