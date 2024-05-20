using CommentService.Application.DTOs.UserDTOs;
using CommentService.Application.Interfaces.Security;
using System.Security.Claims;

namespace CommentService.Infrastructure.Security
{
    public class UserContextProvider : IUserContextProvider
    {
        public async Task<UserContext> GetContext(ClaimsPrincipal userClaims)
        {
            var userIdClaim = userClaims.FindFirst("id");

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                throw new InvalidDataException("Missing credentials.");
            }

            return new UserContext() { UserId = userId };
        }
    }
}
