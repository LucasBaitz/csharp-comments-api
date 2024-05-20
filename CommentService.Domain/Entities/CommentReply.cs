namespace CommentService.Domain.Entities
{
    public class CommentReply
    {
        public Guid CommentId { get; set; }
        public Guid ReplyCommentId { get; set; }
        public Comment Comment { get; set; } = null!;
        public Comment ReplyComment { get; set; } = null!;
    }
}
