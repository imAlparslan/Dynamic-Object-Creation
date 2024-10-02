using DynamicObjectBuilder.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DynamicObjectBuilder.Business;
public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        services.AddScoped<ISchemaBuilderService, SchemaBuilderService>();
        services.AddScoped<IEntityService, EntityService>();


        services.AddAutoMapper(Assembly.GetExecutingAssembly());


        return services;
    }
}
