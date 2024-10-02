namespace DynamicObjectBuilder.Contracts.Common;
public class DynamicSchemaResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<SchemaFieldResponse> Fields { get; set; } 
}
