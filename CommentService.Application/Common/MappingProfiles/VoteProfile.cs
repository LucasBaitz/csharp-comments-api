using AutoMapper;
using CommentService.Application.DTOs.Comment;
using CommentService.Domain.Entities;

namespace CommentService.Application.Common.MappingProfiles
{
    public class VoteProfile : Profile
    {
        public VoteProfile()
        {
            CreateMap<CommentVote, VoteDTO>();
        }
    }
}
