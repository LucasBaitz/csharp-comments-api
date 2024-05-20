using CommentService.Application.DTOs.Comment;
using CommentService.Application.DTOs.UserDTOs;
using CommentService.Domain.Entities;

namespace CommentService.Application.Interfaces
{
    public interface ICommentsService
    {
        Task<CommentDTO> GetById(Guid id);
        Task<IEnumerable<CommentDTO>> GetAll();
        Task<IEnumerable<CommentDTO>> GetAllByUserId(Guid userId);
        Task<Comment> Add(CreateCommentDTO commentDto, UserContext userContext);
        Task VoteComment(Guid commentId, VoteCommentDTO voteDto, UserContext userContext);
        Task<bool> HasUserVoted(Guid commentId, UserContext userContext);
        Task<Comment> ReplyComment(Guid commentId, CreateCommentDTO replyCommentDTO, UserContext userContext);
        Task Update(Guid commentId, CreateCommentDTO comment, UserContext userContext);
        Task Delete(Guid id);
    }
}
