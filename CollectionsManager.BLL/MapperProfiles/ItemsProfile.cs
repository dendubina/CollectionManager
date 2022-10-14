using AutoMapper;
using CollectionsManager.BLL.DTO.Items;
using CollectionsManager.DAL.Entities;

namespace CollectionsManager.BLL.MapperProfiles
{
    public class ItemsProfile : Profile
    {
        public ItemsProfile()
        {
            CreateMap<Item, ItemInCollectionDetailsDto>()
                .ForMember(x => x.CustomFieldValues, opt => opt.MapFrom(x => x.CustomValues));

            CreateMap<ItemToCreate, Item>()
                .ForMember(x => x.CustomValues, opt => opt.MapFrom(x => x.CustomFieldValuesToCreate))
                .ForMember(x => x.Tags, opt => opt.Ignore());

            CreateMap<Item, ItemDetailsToReturnDto>()
                .ForMember(x => x.CollectionName, opt => opt.MapFrom(x => x.Collection.Name))
                .ForMember(x => x.OwnerName, opt => opt.MapFrom(x => x.Collection.Owner.UserName))
                .ForMember(x => x.CustomFieldValues, opt => opt.MapFrom(x => x.CustomValues));

            CreateMap<Item, ItemToEditDto>()
                .ForMember(x => x.CustomFields, opt => opt.MapFrom(x => x.CustomValues));

            CreateMap<ItemToEditDto, Item>()
                .ForMember(x => x.CustomValues, opt => opt.MapFrom(x => x.CustomFields))
                .ForMember(x => x.Tags, opt => opt.Ignore());
        }
    }
}
