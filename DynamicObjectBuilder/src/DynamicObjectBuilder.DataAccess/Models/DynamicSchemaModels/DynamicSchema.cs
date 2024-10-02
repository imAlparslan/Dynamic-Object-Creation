namespace DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;

public sealed class DynamicSchema
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public List<SchemaField> Fields { get; private set; } = new();


    public DynamicSchema(string name, List<SchemaField> fields, Guid? id = null)
    {
        Name = name;
        Fields = fields;
        Id = id ?? Guid.NewGuid();
    }

    private DynamicSchema()
    {
        
    }
}

