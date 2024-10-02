using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DynamicObjectBuilder.DataAccess.Repositories;
internal class SchemaRepository : ISchemaRepository
{
    private readonly SchemaBuilderDbContext _context;

    public SchemaRepository(SchemaBuilderDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateTableAsync(string schemaName, CancellationToken cancellationToken)
    {
        StringBuilder query = new StringBuilder();
        query.AppendFormat("CREATE TABLE {0}", schemaName);
        query.AppendLine("(ID INTEGER IDENTITY(1, 1) NOT NULL,");

        //other columns
        query.AppendLine("TEsT NVARCHAR(MAX) NOT NULL,");


        query.AppendFormat("CONSTRAINT[PK_{0}] PRIMARY KEY([ID]))", schemaName);


        //frogein keys
        //...

        await _context.Database.ExecuteSqlRawAsync(query.ToString(), cancellationToken);

        return true;
    }

    public async Task<bool> IsSNameExists()
    {

        return true;
    }
}

