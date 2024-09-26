using AutoMapper;
using DynamicObjectBuilder.Business.Exceptions;
using DynamicObjectBuilder.Contracts.DynamicEntityRequests;
using DynamicObjectBuilder.DataAccess;
using DynamicObjectBuilder.DataAccess.Models.DynamicEntityModels;
using DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DynamicObjectBuilder.Business.Services;
public class DynamicEntityService : IDynamicEntityService
{
    private readonly SchemaBuilderDbContext _dbContext;
    private readonly IMapper _mapper;

    public DynamicEntityService(SchemaBuilderDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<DynamicEntity> CreateDynamicEntityAsync(CreateDynamicEntityRequest request, CancellationToken cancellationToken)
    {
        var schema = await EnsureSchemaExists(request.SchemaId, cancellationToken);

        await EnsureRequiredSchemaFieldsSended(request, schema);

        await EnsureRequestDoesNotHaveUnknownField(request, schema);

        var rootEntityFields = new List<EntityField>();

        foreach (var item in request.Fields)
        {
            if (item.Value.EntityField is not null)
            {
                var subSchema = await EnsureSchemaExists(item.Value.EntityField.SchemaId, cancellationToken);

                await EnsureRequiredSchemaFieldsSended(item.Value.EntityField, subSchema);

                await EnsureRequestDoesNotHaveUnknownField(item.Value.EntityField, subSchema);


                var subEntity = _mapper.Map<DynamicEntity>(item.Value.EntityField);

                await _dbContext.DynamicEntity.AddAsync(subEntity, cancellationToken);

                var rootFieldValue = new FieldValue(dynamicEntityId: subEntity.Id);

                var rootField = new EntityField(item.SchemaTypeId, item.FieldIdentifier, rootFieldValue);

                rootEntityFields.Add(rootField);
            }
            else
            {
                FieldValue value = _mapper.Map<FieldValue>(item.Value);

                rootEntityFields.Add(new EntityField(item.SchemaTypeId, item.FieldIdentifier, value));
            }
        }

        var dynamicEntity = new DynamicEntity(request.SchemaId, rootEntityFields);

        await _dbContext.DynamicEntity.AddAsync(dynamicEntity, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return dynamicEntity;
    }

    public async Task<bool> DeleteEntityById(Guid entityId, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.DynamicEntity.FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);

        if (entity is null)
        {
            throw new DynamicEntityExeception("entity not found");
        }

        var usedIn = await _dbContext.DynamicEntity
                .Include(x => x.Fields)
                    .SelectMany(x => x.Fields!, (entity, field) => new { entity, field })
                    .Where(x => x.field.Value.DynamicEntityId != null && x.field.Value.DynamicEntityId == entityId)
                    .Select(x => x.entity)
                    .ToListAsync(cancellationToken);

        if (!usedIn.IsNullOrEmpty())
        {
            throw new DynamicEntityExeception("This entity cannot deleted because it is in use");
        }

        _dbContext.DynamicEntity.Remove(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;

    }

    public async Task<DynamicEntity> GetDynamicEntityByEntityIdAsync(Guid entityId, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.DynamicEntity.FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);
        if (entity is null)
        {
            throw new DynamicEntityExeception("Entity not found");
        }
        return entity;
    }

    public async Task<IEnumerable<DynamicEntity>> GetDynamicEntityBySchemaIdAsync(Guid schemaId, CancellationToken cancellationToken)
    {
        return await _dbContext.DynamicEntity.Where(x => x.SchemaId == schemaId)
            .ToListAsync(cancellationToken);
    }

    private async Task EnsureRequestDoesNotHaveUnknownField(CreateDynamicEntityRequest request, DynamicSchema schema)
    {
        var schemaFieldTypes = schema.Fields.Select(x => x.SchemaTypeId).ToList();
        var requestFieldTypes = request.Fields.Select(x => x.SchemaTypeId).ToList();

        var unknownSchemaFields = schemaFieldTypes.Except(requestFieldTypes).ToList();
        if (!unknownSchemaFields.IsNullOrEmpty())
        {
            var unkownValues = request.Fields.Where(x => unknownSchemaFields.Contains(x.FieldIdentifier)).Select(x => x.Value);
            throw new DynamicEntityExeception($"Unknown fields for {string.Join(", ", unkownValues)}");
        }

        await Task.CompletedTask;
    }
    private async Task EnsureRequiredSchemaFieldsSended(CreateDynamicEntityRequest request, DynamicSchema schema)
    {
        var requiredFields = schema.Fields.Where(x => x.IsRequired);

        var fields = request.Fields.Select(x => x.FieldIdentifier);


        if (requiredFields is not null)
        {
            var missingFields = requiredFields.Select(x => x.FieldIdentifier)
                .Except(fields)
                .ToList();

            if (!missingFields.IsNullOrEmpty())
            {
                var missingFieldNames = requiredFields.Where(x => missingFields.Contains(x.FieldIdentifier))
                                                            .Select(x => x.Name).
                                                             ToList();

                throw new DynamicEntityExeception($"Missing field ({string.Join(", ", missingFieldNames)})");
            }
        }

        await Task.CompletedTask;

    }
    private async Task<DynamicSchema> EnsureSchemaExists(Guid schemaId, CancellationToken cancellationToken)
    {
        var schema = await _dbContext.DynamicSchemas.FirstOrDefaultAsync(x => x.Id == schemaId, cancellationToken);
        if (schema is null)
        {
            throw new DynamicEntityExeception("Unknown schema");
        }
        return schema;
    }
}
