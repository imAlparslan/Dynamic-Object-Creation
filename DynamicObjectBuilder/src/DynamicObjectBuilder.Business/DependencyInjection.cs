using DynamicObjectBuilder.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicObjectBuilder.Business;
public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        services.AddScoped<ISchemaBuilderService, SchemaBuilderService>();

        return services;
    }
}
