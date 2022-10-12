using System;
using AutoMapper;
using CollectionsManager.BLL.DTO.Comments;
using CollectionsManager.DAL.Entities;

namespace CollectionsManager.BLL.MapperProfiles
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile()
        {
            CreateMap<Comment, CommentToReturnDto>()
                .ForMember(x => x.AuthorName, opt => opt.MapFrom(x => x.Author.UserName));

            CreateMap<CommentToCreateDto, Comment>()
                .ForMember(x => x.Text, opt => opt.MapFrom(x => x.CommentText))
                .ForMember(x => x.Date, opt => opt.MapFrom(x => DateTime.Now));
        }
    }
}
