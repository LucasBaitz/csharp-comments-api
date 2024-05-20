

namespace CommentService.Application.DTOs.Comment
{
    public record CreateCommentDTO
    {
        public string Content { get; set; } = string.Empty;
    }
}
