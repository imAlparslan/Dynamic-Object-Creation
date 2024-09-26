namespace DynamicObjectBuilder.DataAccess.Models.DynamicEntityModels;

public sealed class EntityField
{
    public Guid SchemaTypeId { get; set; }
    public Guid FieldIdentifier { get; set; }
    public FieldValue Value { get; set; }

    public EntityField(Guid schemaTypeId, Guid fieldIdentifier, FieldValue fieldValue)
    {
        SchemaTypeId = schemaTypeId;
        FieldIdentifier = fieldIdentifier;
        Value = fieldValue;
    }

    private EntityField()
    {

    }
}