using DynamicObjectBuilder.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicObjectBuilder.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<SchemaBuilderDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString: configuration.GetConnectionString("MSSQL"));

            });

            services.AddScoped<ISchemaRepository, SchemaRepository>();
            return services;
        }
    }
}
