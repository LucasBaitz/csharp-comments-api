using CommentService.Domain.Entities.Enums;

namespace CommentService.Domain.Entities
{
    public class CommentVote
    {
        public Guid Id { get; set; }
        public Guid CommentId { get; set; }
        public virtual Comment Comment { get; set; } = null!;
        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public VoteType Vote { get; set; }
    }
}
