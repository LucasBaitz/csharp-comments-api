using CommentService.Application.DTOs.UserDTOs;
using System.Security.Claims;

namespace CommentService.Application.Interfaces.Security
{
    public interface IUserContextProvider
    {
        Task<UserContext> GetContext(ClaimsPrincipal userClaims);
    }
}
