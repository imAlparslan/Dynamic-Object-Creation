using AutoMapper;
using DynamicObjectBuilder.Business.Common;
using DynamicObjectBuilder.Business.Exceptions;
using DynamicObjectBuilder.Contracts.Common;
using DynamicObjectBuilder.Contracts.SchemaBuilderRequests.CreateSchema;
using DynamicObjectBuilder.DataAccess;
using DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;
using DynamicObjectBuilder.DataAccess.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DynamicObjectBuilder.Business.Services
{
    public sealed class SchemaBuilderService : ISchemaBuilderService
    {
        private readonly SchemaBuilderDbContext _dbContext;
        private readonly IMapper _mapper;
        public SchemaBuilderService(SchemaBuilderDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<DynamicSchemaResponse> CreateSchemaAsync(CreateSchemaRequest request, CancellationToken cancellationToken)
        {
            string formattedTableName = await ValidateAndFormatTableName(request, cancellationToken);

            var duplicateFieldNames = GetDuplicateFieldNames(request);

            if (!duplicateFieldNames.IsNullOrEmpty())
            {
                throw new SchemaException($"Duplicate Field names on {string.Join(", ", duplicateFieldNames)}");
            }

            List<SchemaField> fields = new();

            DynamicSchema schema = new DynamicSchema(formattedTableName, fields, Guid.NewGuid());

            fields.AddRange(request.Fields
                                .Select(x => SchemaFieldRequestToSchemaField(x, schema))
                                .ToList());

            var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                await _dbContext.DynamicSchemas.AddAsync(schema, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);

                await CreateTable(schema, cancellationToken);


                transaction.Commit();

                return _mapper.Map<DynamicSchemaResponse>(schema);
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        private static List<string> GetDuplicateFieldNames(CreateSchemaRequest request)
        {
            return request.Fields.GroupBy(x => x.Name, (key, list) => new { key, list })
                        .Where(x => x.list.Count() > 1)
                        .Select(x => x.key)
                        .ToList();
        }

        private async Task<string> ValidateAndFormatTableName(CreateSchemaRequest request, CancellationToken cancellationToken)
        {
            var formattedTableName = SqlHelper.SchemaNameToSqlTableName(request.SchemaName);

            var isSchemaExists = await _dbContext.DynamicSchemas.AnyAsync(x => x.Name == formattedTableName, cancellationToken);

            if (isSchemaExists)
            {
                throw new SchemaException("Schema name should be unique");
            }

            return formattedTableName;
        }

        private async Task CreateTable(DynamicSchema schema, CancellationToken cancellationToken)
        {
            var sql = SqlHelper.DynamicSchemaToRawSql(schema);
            await _dbContext.Database.ExecuteSqlRawAsync(sql, cancellationToken);
        }

        private SchemaField SchemaFieldRequestToSchemaField(CreateSchemaFieldRequest request, DynamicSchema owner)
        {
            if (request.DynamicSchemaId.HasValue)
            {
                var subSchema = _dbContext.DynamicSchemas.FirstOrDefault(x => x.Id == request.DynamicSchemaId);

                if (subSchema is null)
                {
                    throw new SchemaException($"{request.Name} has unknown type");
                }
                return new SchemaField(request.Name, request.IsRequired, owner, (FieldType)request.FieldType, dynamicSchema: subSchema);
            }
            return new SchemaField(request.Name, request.IsRequired, owner, (FieldType)request.FieldType);
        }

        public async Task<DynamicSchemaResponse> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            var schema = await _dbContext.DynamicSchemas.AsNoTracking()
                .Include(x => x.Fields)
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);

            if (schema is null)
            {
                throw new SchemaException("Unknown schema");
            }

            return _mapper.Map<DynamicSchemaResponse>(schema);
        }

        public async Task<bool> DeleteByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            var schema = await _dbContext.DynamicSchemas
               .Include(x => x.Fields)
               .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);

            if (schema is null)
            {
                throw new SchemaException("Unknown schema");
            }
            var tableName = SqlHelper.SchemaNameToSqlTableName(schema.Name);

            var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync($"DROP TABLE [{tableName}]", cancellationToken);
                _dbContext.DynamicSchemas.Remove(schema);
                await _dbContext.SaveChangesAsync(cancellationToken);

                transaction.Commit();

            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            return true;
        }

        public async Task<List<DynamicSchemaResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var schemas = await _dbContext.DynamicSchemas
                .AsNoTracking()
                .Include(x => x.Fields)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<DynamicSchemaResponse>>(schemas);
        }
    }
}