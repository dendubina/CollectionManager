using AutoMapper;
using CollectionManager.WEB.Models.Items;
using CollectionManager.WEB.Models.Tags;
using CollectionsManager.BLL.DTO.Items;
using CollectionsManager.BLL.DTO.Tags;

namespace CollectionManager.WEB.Mapper
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<TagDto, ExistedTagToEditViewModel>();

            CreateMap<ItemToEditDto, ItemToEditViewModel>()
                .ForMember(x => x.ExistedTags, opt => opt.MapFrom(x => x.Tags));

            CreateMap<ItemToEditViewModel, ItemToEditDto>();
        }
    }
}
