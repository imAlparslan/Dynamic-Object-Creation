using DynamicObjectBuilder.Business;
using DynamicObjectBuilder.DataAccess;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();


builder.Services
    .AddBusinessLayer()
    .AddDataAccessLayer(builder.Configuration);

builder.Services
    .AddGraphQLServer()
    .AddApiTypes()
    .ModifyOptions(x =>
    {
        x.EnableOneOf = true;
    })
    .AddMutationConventions()
    .AddErrorFilter(error =>
    {
        return error.WithMessage("something went wrong")
            .RemoveExtensions()
            .RemovePath()
            .RemoveLocations();
    })
    .InitializeOnStartup();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SchemaBuilderDbContext>();
    context.Database.Migrate();
}

app.MapGraphQL();

app.Run();