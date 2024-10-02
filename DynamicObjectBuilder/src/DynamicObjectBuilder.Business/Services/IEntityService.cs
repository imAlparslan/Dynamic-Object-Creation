using DynamicObjectBuilder.Contracts.DynamicEntityRequests.CreateEntity;

namespace DynamicObjectBuilder.Business.Services
{
    public interface IEntityService
    {
        public Task<Guid> CreateEntityAsync(CreateDynamicEntityRequest request, CancellationToken cancellationToken);
    }
}
