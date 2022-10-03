using AutoMapper;
using CollectionManager.WEB.Models.Collections;
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

            CreateMap<Collection, UsersCollectionToShow>()
                .ForMember(x => x.ItemsCount, opt => opt.MapFrom(x => x.Items.Count));
        }
    }
}
