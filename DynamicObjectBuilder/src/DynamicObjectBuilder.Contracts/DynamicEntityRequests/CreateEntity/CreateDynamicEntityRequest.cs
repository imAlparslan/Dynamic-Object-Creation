namespace DynamicObjectBuilder.Contracts.DynamicEntityRequests.CreateEntity;
public class CreateDynamicEntityRequest
{
    public Guid SchemaId { get; private set; }
    public List<EntityFieldRequest> Fields { get; private set; } = null!;
    public CreateDynamicEntityRequest(Guid schemaId, List<EntityFieldRequest> fields)
    {
        SchemaId = schemaId;
        Fields = fields;
    }
}
