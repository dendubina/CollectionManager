using AutoMapper;
using CollectionManager.WEB.Models.Items;
using Entities.DTO.Items;
using Entities.EF.Models;

namespace CollectionManager.WEB.Models.MapperProfile
{
    public class ItemsProfile : Profile
    {
        public ItemsProfile()
        {
            CreateMap<Item, ItemInCollectionDetails>()
                .ForMember(x => x.CustomFieldValues, opt => opt.MapFrom(x => x.CustomValues));

            CreateMap<ItemToCreate, Item>()
                .ForMember(x => x.CustomValues, opt => opt.MapFrom(x => x.CustomFieldValuesToCreate));

            CreateMap<Item, ItemDetailsToReturnDto>()
                .ForMember(x => x.CollectionName, opt => opt.MapFrom(x => x.Collection.Name))
                .ForMember(x => x.OwnerName, opt => opt.MapFrom(x => x.Collection.Owner.UserName))
                .ForMember(x => x.CustomFieldValues, opt => opt.MapFrom(x => x.CustomValues));

            CreateMap<Item, ItemToEditDto>()
                .ForMember(x => x.CustomFields, opt => opt.MapFrom(x => x.CustomValues))
                .ReverseMap();
        }
    }
}
