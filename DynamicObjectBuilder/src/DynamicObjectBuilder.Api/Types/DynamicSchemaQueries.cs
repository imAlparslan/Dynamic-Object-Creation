using DynamicObjectBuilder.Business.Services;
using DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;

namespace DynamicObjectBuilder.Api.Types;

[QueryType]
public static class DynamicSchemaQueries
{
    public static async Task<IEnumerable<DynamicSchema>> GetAllSchemas(
        [Service] ISchemaBuilderService schemaBuilderService,
        CancellationToken cancellationToken)
    {
        return await schemaBuilderService.GetAllAsync(cancellationToken);
    }
}
