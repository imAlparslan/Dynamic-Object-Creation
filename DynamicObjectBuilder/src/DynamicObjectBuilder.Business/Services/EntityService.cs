using DynamicObjectBuilder.Business.Common;
using DynamicObjectBuilder.Business.Exceptions;
using DynamicObjectBuilder.Contracts.DynamicEntityRequests.CreateEntity;
using DynamicObjectBuilder.DataAccess;
using DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace DynamicObjectBuilder.Business.Services;
public class EntityService : IEntityService
{
    private readonly SchemaBuilderDbContext _dbContext;

    public EntityService(SchemaBuilderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> CreateEntityAsync(CreateDynamicEntityRequest request, CancellationToken cancellationToken)
    {
        var schema = await _dbContext.DynamicSchemas.Include(x => x.Fields)
                    .FirstOrDefaultAsync(x => x.Id == request.SchemaId, cancellationToken);

        if (schema is null)
        {
            throw new SchemaException("Unknown schema");
        }

        EnsureRequiredFiedsSended(request, schema);

        var columnNames = new List<string>();
        var values = new List<object>();

        var primitiveFields = request.Fields.Where(x => x.Value.EntityField is null).ToList();

        foreach (var field in primitiveFields)
        {
            var column = schema.Fields.FirstOrDefault(x => x.FieldIdentifer == field.FieldIdentifier);
            if (column is null)
            {
                throw new SchemaException($"Unknown field");
            }
            columnNames.Add(column.Name);
            values.Add(GetValueFromEntityFieldValueRequest(field.Value));

        }

        var subFields = request.Fields.Where(x => x.Value.EntityField is not null).ToList();

        foreach (var field in subFields)
        {
            var column = schema.Fields.FirstOrDefault(x => x.FieldIdentifer == field.FieldIdentifier);
            if (column is null)
            {
                throw new SchemaException($"Unknown field");
            }
            var subId = await InsertCreateDynamicEntityRequest(field.Value.EntityField!, cancellationToken);
            columnNames.Add(column.Name);
            values.Add($"'{subId}'");

        }

        var columns = columnNames
            .Aggregate((x, y) => $"[{SqlHelper.FieldNameToSqlColumnName(x)}], [{SqlHelper.FieldNameToSqlColumnName(y)}]");


        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendJoin(", ", values);


        var valuesAsStr = stringBuilder.ToString();

        var id = Guid.NewGuid();
        var sql = $"INSERT INTO {schema.Name} ([Id], {columns}) VALUES('{id}' ,{valuesAsStr})";

        var transaction = _dbContext.Database.BeginTransaction();
        try
        {
            await _dbContext.Database.ExecuteSqlRawAsync(sql, cancellationToken);
            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }

        return id;
    }

    private static void EnsureRequiredFiedsSended(CreateDynamicEntityRequest request, DynamicSchema? schema)
    {
        var requiredFields = schema.Fields.Where(x => x.IsRequired)
            .Select(x => x.FieldIdentifer)
            .ToList();

        var requestFields = request.Fields.Select(x => x.FieldIdentifier)
            .ToList();

        var missingFields = requiredFields.Except(requestFields);

        if (missingFields.Any())
        {
            throw new SchemaException($"missing fields");
        }
    }

    private async Task<Guid> InsertCreateDynamicEntityRequest(CreateDynamicEntityRequest request, CancellationToken cancellationToken)
    {

        var schema = await _dbContext.DynamicSchemas.Include(x => x.Fields)
                   .FirstOrDefaultAsync(x => x.Id == request.SchemaId, cancellationToken);

        if (schema is null)
        {
            throw new SchemaException("Unknown schema");
        }

        EnsureRequiredFiedsSended(request, schema);

        var columnNames = new List<string>();
        var values = new List<object>();

        var primitiveFields = request.Fields.Where(x => x.Value.EntityField is null).ToList();

        foreach (var field in primitiveFields)
        {
            var column = schema.Fields.FirstOrDefault(x => x.FieldIdentifer == field.FieldIdentifier);
            if (column is null)
            {
                throw new SchemaException($"Unknown field");
            }
            columnNames.Add(column.Name);
            values.Add(GetValueFromEntityFieldValueRequest(field.Value));

        }

        var subFields = request.Fields.Where(x => x.Value.EntityField is not null).ToList();

        foreach (var field in subFields)
        {
            var column = schema.Fields.FirstOrDefault(x => x.FieldIdentifer == field.FieldIdentifier);
            if (column is null)
            {
                throw new SchemaException($"Unknown field");
            }
            var subId = await InsertCreateDynamicEntityRequest(field.Value.EntityField!, cancellationToken);
            columnNames.Add(column.Name);
            values.Add($"'{subId}'");

        }

        var columns = columnNames
            .Aggregate((x, y) => $"[{SqlHelper.FieldNameToSqlColumnName(x)}], [{SqlHelper.FieldNameToSqlColumnName(y)}]");


        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendJoin(", ", values);

        var valuesAsStr = stringBuilder.ToString();

        var id = Guid.NewGuid();

        var sql = $"INSERT INTO {schema.Name} ([Id], {columns}) VALUES('{id}' ,{valuesAsStr})";

        var transaction = _dbContext.Database.BeginTransaction();
        try
        {
            await _dbContext.Database.ExecuteSqlRawAsync(sql, cancellationToken);
            transaction.Commit();

            return id;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    private object GetValueFromEntityFieldValueRequest(EntityFieldValueRequest entityFieldValueRequest)
    {

        if (entityFieldValueRequest.Number.HasValue)
        {
            return entityFieldValueRequest.Number.Value;
        }
        if (entityFieldValueRequest.Boolean.HasValue)
        {
            return entityFieldValueRequest.Boolean.Value;
        }
        if (!string.IsNullOrEmpty(entityFieldValueRequest.Text))
        {
            return $"'{entityFieldValueRequest.Text}'";
        }
        if (entityFieldValueRequest.Decimal.HasValue)
        {
            return $"{entityFieldValueRequest.Decimal.Value.ToString(CultureInfo.InvariantCulture)}";
        }
        throw new SchemaException("Invalid Request");
    }
}
