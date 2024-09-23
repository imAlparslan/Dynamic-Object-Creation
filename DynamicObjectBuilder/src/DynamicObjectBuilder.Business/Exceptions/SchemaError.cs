namespace DynamicObjectBuilder.Business.Exceptions;
public class SchemaError
{
    public string Message { get; set; }


    public SchemaError(string message)
    {
        Message = message;
    }

    public static SchemaError? CreateErrorFrom(SchemaException exception)
    {
        return new SchemaError(exception.Message);
    }

}
