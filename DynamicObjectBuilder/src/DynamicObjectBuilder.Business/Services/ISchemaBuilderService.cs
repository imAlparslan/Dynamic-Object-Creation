using DynamicObjectBuilder.Contracts.SchemaBuilderRequests;
using DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;

namespace DynamicObjectBuilder.Business.Services;
public interface ISchemaBuilderService
{
    Task<DynamicSchema> CreateSchemaAsync(CreateSchemaRequest request, CancellationToken cancellationToken);
    Task<DynamicSchema> UpdateSchemaAsync(UpdateSchemaRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<DynamicSchema> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<IEnumerable<DynamicSchema>> GetAllAsync(CancellationToken cancellationToken);

}
