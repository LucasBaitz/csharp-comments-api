using CommentService.Domain.Entities.Enums;

namespace CommentService.Application.DTOs.Comment
{
    public record VoteCommentDTO
    {
        public VoteType Vote { get; set; }
    }
}
