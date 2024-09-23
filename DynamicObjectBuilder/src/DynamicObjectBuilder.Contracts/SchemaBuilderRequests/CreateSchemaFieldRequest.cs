namespace DynamicObjectBuilder.Contracts.SchemaBuilderRequests;
public class CreateSchemaFieldRequest
{
    public Guid SchemaTypeId { get; set; }
    public string Name { get; set; }
    public bool IsRequired { get; set; }

}
