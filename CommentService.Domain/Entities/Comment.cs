using CommentService.Domain.Entities.Enums;

namespace CommentService.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int Score { get; set; }
        public Guid UserId { get; set; }
        public CommentType CommentType { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment> Replies { get; set; } = null!;
        public virtual ICollection<CommentVote> Votes { get; set; } = null!;
    }
}
