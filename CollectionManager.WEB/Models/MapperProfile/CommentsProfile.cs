using System;
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

            CreateMap<CommentToCreateDto, Comment>()
                .ForMember(x => x.Text, opt => opt.MapFrom(x => x.CommentText))
                .ForMember(x => x.Date, opt => opt.MapFrom(x => DateTime.Now));
        }
    }
}
