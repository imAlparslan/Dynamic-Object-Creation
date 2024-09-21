using DynamicObjectBuilder.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<SchemaBuilderDbContext>(opt =>
{
    opt.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("MSSQL"));
});


var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
