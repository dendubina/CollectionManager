using AutoMapper;
using Entities.DTO.Collections;
using Entities.DTO.CustomFields;
using Entities.EF.Models;

namespace CollectionManager.WEB.Models.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CustomFieldToCreate, CustomField>()
                .ForMember(x => x.FieldType, opt => opt.MapFrom(x => x.Type));

            CreateMap<CollectionToCreateDto, Collection>();
        }
    }
}
