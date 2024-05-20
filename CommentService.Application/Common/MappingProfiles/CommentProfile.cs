using AutoMapper;
using CommentService.Application.DTOs.Comment;
using CommentService.Domain.Entities;

namespace CommentService.Application.Common.MappingProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CreateCommentDTO, Comment>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
        }
    }
}
