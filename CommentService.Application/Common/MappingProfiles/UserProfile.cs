using AutoMapper;
using CommentService.Application.DTOs.UserDTOs;
using CommentService.Domain.Entities;

namespace CommentService.Application.Common.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateAccountDTO, User>();
            CreateMap<User, SuccessfulLoginDTO>();
            CreateMap<User, AuthorUserDTO>().ReverseMap();
        }
    }
}
