﻿using DynamicObjectBuilder.Contracts.SchemaBuilderRequests;
using DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;

namespace DynamicObjectBuilder.Business.Services;
public interface ISchemaBuilderService
{
    Task<DynamicSchema> CreateSchemaAsync(CreateSchemaRequest request, CancellationToken cancellationToken);
    //Task<DynamicSchema> UpdateSchemaAsync(UpdateSchemaRequest request, CancellationToken cancellationToken);
    //Task DeleteById(Guid Id);
    //Task GetById(Guid Id);
    Task<IEnumerable<DynamicSchema>> GetAllAsync(CancellationToken cancellationToken);

}
