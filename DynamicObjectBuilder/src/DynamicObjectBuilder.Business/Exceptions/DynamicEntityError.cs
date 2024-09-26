namespace DynamicObjectBuilder.Business.Exceptions;
public class DynamicEntityError
{
    public string Message { get; set; }
    public DynamicEntityError(string message)
    {
        Message = message;
    }
    public static DynamicEntityError? CreateErrorFrom(DynamicEntityExeception exception)
    {
        return new DynamicEntityError(exception.Message);
    }

}
