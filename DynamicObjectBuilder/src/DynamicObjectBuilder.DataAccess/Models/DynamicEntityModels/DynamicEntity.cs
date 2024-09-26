namespace DynamicObjectBuilder.DataAccess.Models.DynamicEntityModels;
public sealed class DynamicEntity
{
    public Guid Id { get; set; }
    public Guid SchemaId { get; set; }
    public List<EntityField>? Fields { get; set; } = new();
    public DynamicEntity(Guid schemaId, List<EntityField> fields, Guid? id = null)
    {
        Fields = fields;
        SchemaId = schemaId;
        Id = id ?? Guid.NewGuid();
    }

    private DynamicEntity()
    {

    }

}
