using DynamicObjectBuilder.Business.Exceptions;
using DynamicObjectBuilder.Contracts.SchemaBuilderRequests;
using DynamicObjectBuilder.DataAccess;
using DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DynamicObjectBuilder.Business.Services
{
    public sealed class SchemaBuilderService : ISchemaBuilderService
    {
        private readonly SchemaBuilderDbContext _dbContext;
        public SchemaBuilderService(SchemaBuilderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DynamicSchema> CreateSchemaAsync(CreateSchemaRequest request, CancellationToken cancellationToken)
        {

            List<SchemaField> newSchemaFields = request.Fields
                        .Select(x => new SchemaField(x.Name, x.IsRequired, x.SchemaTypeId))
                        .ToList();



            await IsNewSchemaNameExists(request.SchemaName, cancellationToken);
            
            await CheckDuplicateFieldNameSended(newSchemaFields);

            await CheckAllFieldsAreKnown(newSchemaFields, cancellationToken);



            DynamicSchema newSchema = new DynamicSchema(request.SchemaName, newSchemaFields);

            await _dbContext.DynamicSchemas.AddAsync(newSchema, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return newSchema;
        }

        public async Task<IEnumerable<DynamicSchema>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.DynamicSchemas.AsQueryable()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        private async Task IsNewSchemaNameExists(string schemaName, CancellationToken cancellationToken)
        {
            var isExists = await _dbContext.DynamicSchemas.AnyAsync(x => x.Name == schemaName, cancellationToken);

            if (isExists)
            {
                throw new SchemaException($"Given schema name already exists {schemaName}");
            }

        }
        private Task CheckDuplicateFieldNameSended(List<SchemaField> fields)
        {
            var duplicates = fields.GroupBy(x => x.Name)
                .Where(x => x.Count() > 1)
                .Select(x => x.Key)
                .ToList();

            if (duplicates.Any())
            {
                throw new SchemaException($"There are duplicate field name {string.Join(", ", duplicates)}");
            }

            return Task.CompletedTask;
        }
        private async Task CheckAllFieldsAreKnown(List<SchemaField> newSchemaFields, CancellationToken cancellationToken)
        {
            var newFieldTypes = newSchemaFields.Select(x => x.SchemaType)
                    .ToList();

            var schemas = await _dbContext.DynamicSchemas
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken);

            var notKnownSchemas = newFieldTypes.Except(schemas).ToList();

            if (!notKnownSchemas.IsNullOrEmpty())
            {
                var unKnownSchemaNames = newSchemaFields.Where(x => notKnownSchemas.Contains(x.SchemaType))
                    .Select(x => x.Name);

                throw new SchemaException($"Unknow field schema {string.Join(", ", unKnownSchemaNames)}");
            }
        }
    }
}
