using AutoMapper;
using CollectionsManager.BLL.DTO.Tags;
using CollectionsManager.DAL.Entities;

namespace CollectionsManager.BLL.MapperProfiles
{
    public class TagsProfile : Profile
    {
        public TagsProfile()
        {
            CreateMap<Tag, TagDto>().ReverseMap();
        }
    }
}
