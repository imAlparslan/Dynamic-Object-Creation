using AutoMapper;
using DynamicObjectBuilder.Business.Common;
using DynamicObjectBuilder.Contracts.Common;
using DynamicObjectBuilder.Contracts.SchemaBuilderRequests.CreateSchema;
using DynamicObjectBuilder.DataAccess.Models.DynamicSchemaModels;

namespace DynamicObjectBuilder.Business.Mapping;
public class Mapper : Profile
{

    public Mapper()
    {
        CreateMap<CreateSchemaFieldRequest, SchemaField>();

        CreateMap<SchemaField, SchemaFieldResponse>()
            .ForMember(x => x.Name, y => y.MapFrom(src => SqlHelper.SqlColumnNameToFieldName(src.Name)));

        CreateMap<DynamicSchema, DynamicSchemaResponse>()
            .ForMember(x => x.Name, y => y.MapFrom(src => SqlHelper.SqlTableNameToSchemaName(src.Name)));

    }
}
