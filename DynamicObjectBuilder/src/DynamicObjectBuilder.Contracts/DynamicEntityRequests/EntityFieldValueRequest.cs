﻿namespace DynamicObjectBuilder.Contracts.DynamicEntityRequests;

[OneOf]
public class EntityFieldValueRequest
{
    public int?  Number { get; set; }
    public string? Text { get; set; }
    public double? Decimal { get; set; }
    public bool? Boolean { get; set; }
    public CreateDynamicEntityRequest? EntityField { get; set; }

}
