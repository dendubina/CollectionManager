using AutoMapper;
using Entities.DTO.Likes;
using Entities.EF.Models;

namespace CollectionManager.WEB.Models.MapperProfile
{
    public class LikesProfile : Profile
    {
        public LikesProfile()
        {
            CreateMap<Like, LikeToReturnDto>();
        }
    }
}
