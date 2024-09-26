using AutoMapper;
using DynamicObjectBuilder.Contracts.DynamicEntityRequests;
using DynamicObjectBuilder.DataAccess.Models.DynamicEntityModels;

namespace DynamicObjectBuilder.Business.Mapping;
public class Mapper : Profile
{

    public Mapper()
    {
        CreateMap<EntityFieldRequest, EntityField>();
        CreateMap<EntityFieldValueRequest, FieldValue>();
        CreateMap<CreateDynamicEntityRequest, DynamicEntity>();
    }

}
