using AutoMapper;
using CollectionManager.WEB.Models.Collections;
using Entities.DTO.Collections;
using Entities.DTO.Items;
using Entities.EF.Models;

namespace CollectionManager.WEB.Models.MapperProfile
{
    public class CollectionsProfile : Profile
    {
        public CollectionsProfile()
        {
            CreateMap<Collection, CollectionToManipulateDto>().ReverseMap();

            CreateMap<Collection, UsersCollectionToShow>()
                .ForMember(x => x.ItemsCount, opt => opt.MapFrom(x => x.Items.Count));

            CreateMap<Collection, CollectionDetailsToShow>()
                .ForMember(x => x.ItemInCollectionDetails, opt => opt.MapFrom(x => x.Items));

            CreateMap<Collection, ItemToCreate>()
                .ForMember(x => x.Name, opt => opt.Ignore())
                .ForMember(x => x.CollectionId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.CustomFieldValuesToCreate, opt => opt.MapFrom(x => x.CustomFields));
        }
    }
}
