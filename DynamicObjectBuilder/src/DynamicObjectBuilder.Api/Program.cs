using DynamicObjectBuilder.DataAccess;
using DynamicObjectBuilder.Business;
using DynamicObjectBuilder.Api.Types;
using HotChocolate.Execution.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();


builder.Services
    .AddBusinessLayer()
    .AddDataAccessLayer(builder.Configuration);

builder.Services
    .AddGraphQLServer()
    .AddApiTypes()
    
    
    //.AddQueryType<Query>()

    //.AddMutationType(typeof(SchemaMutations))
    
    .InitializeOnStartup();
    
    //.AddMutationConventions()
    //.AddMutationType(typeof(SchemaMutations));

var app = builder.Build();

//app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapGraphQL();

app.Run();
