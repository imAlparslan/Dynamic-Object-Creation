namespace DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;

public sealed class SchemaField
{
    public string Name { get; set; }
    public bool IsRequired { get; set; }
    public Guid SchemaTypeId { get; set; }
    public Guid FieldIdentifier { get; set; }

    public SchemaField(string name, bool isRequired, Guid schemaTypeId, Guid? fieldIdentifier = null)
    {
        Name = name;
        IsRequired = isRequired;
        SchemaTypeId = schemaTypeId;
        FieldIdentifier = fieldIdentifier ?? Guid.NewGuid();
    }


    private SchemaField() 
    {
        
    }

}

