using CommentService.Domain.Entities;

namespace CommentService.Application.Interfaces.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
