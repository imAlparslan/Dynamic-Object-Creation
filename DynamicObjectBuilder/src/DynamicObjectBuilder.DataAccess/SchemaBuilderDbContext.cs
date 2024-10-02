using DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DynamicObjectBuilder.DataAccess;
public class SchemaBuilderDbContext : DbContext
{
    public DbSet<DynamicSchema> DynamicSchemas { get; set; }
    //public DbSet<DynamicEntity> DynamicEntity { get; set; }
    public SchemaBuilderDbContext()
    {

    }

    public SchemaBuilderDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
