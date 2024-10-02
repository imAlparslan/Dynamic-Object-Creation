namespace DynamicObjectBuilder.DataAccess.Repositories;
public interface ISchemaRepository
{
    Task<bool> CreateTableAsync(string schemaName, CancellationToken cancellationToken);
}
