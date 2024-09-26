namespace DynamicObjectBuilder.DataAccess.Models.DynamicEntityModels;
public class FieldValue
{
    public int? Number { get; set; }
    public string? Text { get; set; }
    public double? Decimal { get; set; }
    public bool? Boolean { get; set; }
    public Guid? DynamicEntityId { get; set; }

    public FieldValue(int? number = null, string? text = null, double? @decimal = null, bool? boolean = null, Guid? dynamicEntityId = null)
    {
        Number = number;
        Text = text;
        Decimal = @decimal;
        Boolean = boolean;
        DynamicEntityId = dynamicEntityId;
    }

    private FieldValue()
    {

    }
}
