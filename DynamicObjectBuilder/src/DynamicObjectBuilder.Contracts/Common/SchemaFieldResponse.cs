namespace DynamicObjectBuilder.Contracts.Common;
public class SchemaFieldResponse
{
    public Guid Id { get; private set; }
    public DynamicSchemaResponse Owner { get; private set; }
    public string Name { get; private set; }
    public bool IsRequired { get; private set; }
    public int FieldType { get; private set; }
    public DynamicSchemaResponse? DynamicSchema { get; private set; }
}
