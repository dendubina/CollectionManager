using AutoMapper;
using CollectionManager.WEB.Models.Items;
using CollectionManager.WEB.Models.Tags;
using CollectionManager.WEB.Models.Users;
using CollectionsManager.BLL.DTO.Items;
using CollectionsManager.BLL.DTO.Tags;
using CollectionsManager.BLL.DTO.User;

namespace CollectionManager.WEB.Mapper
{
    public class ViewModelsProfile : Profile
    {
        public ViewModelsProfile()
        {
            CreateMap<TagDto, ExistedTagToEditViewModel>();

            CreateMap<ItemToEditDto, ItemToEditViewModel>()
                .ForMember(x => x.ExistedTags, opt => opt.MapFrom(x => x.Tags));

            CreateMap<ItemToEditViewModel, ItemToEditDto>();

            CreateMap<UserToReturnDto, UserViewModel>();
        }
    }
}
