using CommentService.Application.DTOs.UserDTOs;
using CommentService.Domain.Entities;

namespace CommentService.Application.Interfaces.Security
{
    public interface IAuthService
    {
        Task<bool> Register(CreateAccountDTO userDto);
        Task<SuccessfulLoginDTO> Login(CredentialsDTO loginDto);
        Task DeleteAccount(Guid userId, CredentialsDTO loginDto);

    }
}
