using AutoMapper;
using CollectionsManager.BLL.DTO.Likes;
using CollectionsManager.DAL.Entities;

namespace CollectionsManager.BLL.MapperProfiles
{
    public class LikesProfile : Profile
    {
        public LikesProfile()
        {
            CreateMap<Like, LikeToReturnDto>();
        }
    }
}
