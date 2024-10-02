using DynamicObjectBuilder.DataAccess.Models.Enums;

namespace DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;

public sealed class SchemaField
{
    public Guid Id { get; private set; }
    public Guid FieldIdentifer { get; private set; }
    public DynamicSchema Owner { get; private set; }
    public string Name { get; private set; }
    public bool IsRequired { get; private set; }
    public FieldType FieldType { get; private set; }
    public DynamicSchema? DynamicSchema { get; private set; }

    public SchemaField(
        string name,
        bool isRequired,
        DynamicSchema owner,
        FieldType fieldType,
        Guid? id = null,
        Guid? fieldIdentifer = null,
        DynamicSchema? dynamicSchema = null)
    {
        Name = name;
        IsRequired = isRequired;
        Owner = owner;
        FieldType = fieldType;
        DynamicSchema = dynamicSchema;
        FieldIdentifer = fieldIdentifer ?? Guid.NewGuid();
        Id = id ?? Guid.NewGuid();
    }

    private SchemaField()
    {
    }

}

