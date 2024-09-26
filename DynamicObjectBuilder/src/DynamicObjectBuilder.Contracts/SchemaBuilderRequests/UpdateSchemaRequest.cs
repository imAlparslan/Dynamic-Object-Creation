namespace DynamicObjectBuilder.Contracts.SchemaBuilderRequests;
public class UpdateSchemaRequest
{
    public Guid SchemaId { get; set; }
    public string SchemaName { get; set; }
    public List<CreateSchemaFieldRequest> Fields { get; set; }
}
