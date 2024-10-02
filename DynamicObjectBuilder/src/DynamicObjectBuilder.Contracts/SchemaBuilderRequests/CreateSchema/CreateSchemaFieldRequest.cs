namespace DynamicObjectBuilder.Contracts.SchemaBuilderRequests.CreateSchema;
public class CreateSchemaFieldRequest
{
    public string Name { get; set; } = null!;
    public bool IsRequired { get; set; }
    public int FieldType { get; set; }
    public Guid? DynamicSchemaId { get; set; } = null;

}
