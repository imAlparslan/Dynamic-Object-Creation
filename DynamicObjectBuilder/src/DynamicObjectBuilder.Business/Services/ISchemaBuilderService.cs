using DynamicObjectBuilder.Contracts.Common;
using DynamicObjectBuilder.Contracts.SchemaBuilderRequests.CreateSchema;

namespace DynamicObjectBuilder.Business.Services;
public interface ISchemaBuilderService
{
    Task<DynamicSchemaResponse> CreateSchemaAsync(CreateSchemaRequest request, CancellationToken cancellationToken);
    //Task<DynamicSchema> UpdateSchemaAsync(UpdateSchemaRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<DynamicSchemaResponse> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<List<DynamicSchemaResponse>> GetAllAsync(CancellationToken cancellationToken);

}
