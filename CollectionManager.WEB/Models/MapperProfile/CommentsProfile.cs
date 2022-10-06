using AutoMapper;
using Entities.DTO.Comments;
using Entities.EF.Models;

namespace CollectionManager.WEB.Models.MapperProfile
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile()
        {
            CreateMap<Comment, CommentToReturnDto>()
                .ForMember(x => x.AuthorName, opt => opt.MapFrom(x => x.Author.UserName));
        }
    }
}
