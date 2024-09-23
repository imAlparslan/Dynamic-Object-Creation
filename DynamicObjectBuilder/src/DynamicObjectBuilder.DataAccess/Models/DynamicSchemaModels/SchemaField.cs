namespace DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;

public class SchemaField
{
    public string Name { get; set; }
    public bool IsRequired { get; set; }
    public Guid SchemaType { get; set; }
    public Guid FieldIdentifier { get; set; }

    public SchemaField(string name, bool isRequired, Guid schemaType, Guid? fieldIdentifier = null)
    {
        Name = name;
        IsRequired = isRequired;
        SchemaType = schemaType;
        FieldIdentifier = fieldIdentifier ?? Guid.NewGuid();
    }


    public SchemaField()
    {
        
    }

}

