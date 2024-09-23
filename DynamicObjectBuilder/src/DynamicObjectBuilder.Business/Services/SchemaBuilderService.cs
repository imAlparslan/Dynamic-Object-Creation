using DynamicObjectBuilder.Contracts.SchemaBuilderRequests;
using DynamicObjectBuilder.DataAccess;
using DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;
using Microsoft.EntityFrameworkCore;

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
            List<SchemaField> newSchemaFields = request.Fields.Select(x => new SchemaField(x.Name, x.IsRequired, x.SchemaTypeId))
                                                              .ToList();

            DynamicSchema newSchema = new DynamicSchema(request.SchemaName, newSchemaFields);

            await _dbContext.DynamicSchemas.AddAsync(newSchema, cancellationToken);

            await _dbContext.SaveChangesAsync();

            return newSchema;
        }

        public async Task<IEnumerable<DynamicSchema>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.DynamicSchemas.AsQueryable()
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
