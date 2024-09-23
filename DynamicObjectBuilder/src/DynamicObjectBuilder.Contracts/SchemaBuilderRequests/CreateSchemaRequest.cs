namespace DynamicObjectBuilder.Contracts.SchemaBuilderRequests
{
    public class CreateSchemaRequest
    {
        public string SchemaName { get; set; }
        public List<CreateSchemaFieldRequest> Fields { get; set; }
    }
}
