using AutoMapper;
using CollectionsManager.BLL.DTO.Collections;
using CollectionsManager.BLL.DTO.Items;
using CollectionsManager.DAL.Entities;

namespace CollectionsManager.BLL.MapperProfiles
{
    public class CollectionsProfile : Profile
    {
        public CollectionsProfile()
        {
            CreateMap<Collection, CollectionToManipulateDto>().ReverseMap();

            CreateMap<Collection, UsersCollectionToReturnDto>()
                .ForMember(x => x.ItemsCount, opt => opt.MapFrom(x => x.Items.Count));

            CreateMap<Collection, CollectionDetailsToReturnDto>()
                .ForMember(x => x.ItemInCollectionDetails, opt => opt.MapFrom(x => x.Items));

            CreateMap<Collection, ItemToCreate>()
                .ForMember(x => x.Name, opt => opt.Ignore())
                .ForMember(x => x.CollectionId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.CustomFieldValuesToCreate, opt => opt.MapFrom(x => x.CustomFields));

            CreateMap<Collection, LargestCollectionToReturnDto>()
                .ForMember(x => x.OwnerName, opt => opt.MapFrom(x => x.Owner.UserName))
                .ForMember(x => x.OwnerId, opt => opt.MapFrom(x => x.Owner.Id))
                .ForMember(x => x.ItemsCount, opt => opt.MapFrom(x => x.Items.Count));
        }
    }
}
