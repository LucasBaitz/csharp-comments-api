using CommentService.Application.DTOs.UserDTOs;
using CommentService.Domain.Entities.Enums;

namespace CommentService.Application.DTOs.Comment
{
    public record VoteDTO
    {
        public Guid UserId { get; set; }
        public AuthorUserDTO User { get; set; } = null!;
        public VoteType Vote { get; set; }
    }
}
