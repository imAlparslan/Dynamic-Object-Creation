namespace DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;

public class DynamicSchema
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<SchemaFields> Fields { get; set; } = new();
    public bool IsCoreSchema { get; set; }


    public DynamicSchema(string name, List<SchemaFields> fields, bool isCoreSchema = false, Guid? id = null)
    {
        Name = name;
        Fields = fields;
        IsCoreSchema = isCoreSchema;
        Id = id ?? Guid.NewGuid();
    }

    public DynamicSchema()
    {
        
    }
}

