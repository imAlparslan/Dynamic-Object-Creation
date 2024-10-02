namespace DynamicObjectBuilder.Contracts.SchemaBuilderRequests.CreateSchema
{
    public sealed class CreateSchemaRequest
    {
        public string SchemaName { get; set; } = null!;
        public List<CreateSchemaFieldRequest> Fields { get; set; } = null!;
    }
}
