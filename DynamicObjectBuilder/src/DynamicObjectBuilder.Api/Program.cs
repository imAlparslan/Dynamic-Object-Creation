using DynamicObjectBuilder.Business;
using DynamicObjectBuilder.DataAccess;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();


builder.Services
    .AddBusinessLayer()
    .AddDataAccessLayer(builder.Configuration);

builder.Services
    .AddGraphQLServer()
    .AddApiTypes()
    .AddMutationConventions()
    .InitializeOnStartup();

var app = builder.Build();

app.MapGraphQL();

app.Run();
