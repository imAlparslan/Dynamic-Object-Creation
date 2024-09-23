using DynamicObjectBuilder.Business.Services;
using DynamicObjectBuilder.Contracts.SchemaBuilderRequests;
using DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;

namespace DynamicObjectBuilder.Api.Types;

[MutationType]
public static class DynamicSchemaMutations
{
    public static async Task<DynamicSchema> CreateSchema([Service]ISchemaBuilderService schemaBuilderService,
                                             CreateSchemaRequest request,
                                             CancellationToken cancellationToken)
    {
        return await schemaBuilderService.CreateSchemaAsync(request, cancellationToken);

    }

}
