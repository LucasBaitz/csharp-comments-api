using CommentService.Application.DTOs.UserDTOs;
using CommentService.Domain.Entities;

namespace CommentService.Application.DTOs.Comment
{
    public record CommentDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public IEnumerable<VoteDTO> Votes { get; set; } = null!;
        public int Score { get; set; }
        public virtual AuthorUserDTO User { get; set; } = null!;
        public IEnumerable<CommentDTO> Replies { get; set; } = null!;
    }
}
