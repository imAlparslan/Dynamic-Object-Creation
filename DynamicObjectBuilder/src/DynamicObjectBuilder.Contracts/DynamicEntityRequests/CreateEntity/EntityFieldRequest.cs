namespace DynamicObjectBuilder.Contracts.DynamicEntityRequests.CreateEntity;
public class EntityFieldRequest
{
    public int ValueType { get; set; }
    public Guid FieldIdentifier { get; private set; }
    public EntityFieldValueRequest Value { get; set; }



    public EntityFieldRequest(Guid fieldIdentifier, EntityFieldValueRequest value)
    {
        FieldIdentifier = fieldIdentifier;
        Value = value;
    }

    private EntityFieldRequest()
    {

    }
}