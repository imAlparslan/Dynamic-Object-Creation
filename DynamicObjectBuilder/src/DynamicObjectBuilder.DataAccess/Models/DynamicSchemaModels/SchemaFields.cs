namespace DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;

public class SchemaFields
{
    public string Name { get; set; }
    public bool IsRequired { get; set; }
    public Guid SchemaType { get; set; }
    public Guid FieldIdentifier { get; set; }

    public SchemaFields(string name, bool isRequired, Guid? schemaType, Guid? fieldIdentifier)
    {
        Name = name;
        IsRequired = isRequired;
        SchemaType = schemaType ?? Guid.NewGuid();
        FieldIdentifier = fieldIdentifier ?? Guid.NewGuid();
    }


    public SchemaFields()
    {
        
    }

}

