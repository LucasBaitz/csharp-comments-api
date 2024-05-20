using AutoMapper;
using CommentService.Application.DTOs.Comment;
using CommentService.Application.DTOs.UserDTOs;
using CommentService.Application.Interfaces;
using CommentService.Domain.Entities;
using CommentService.Domain.Entities.Enums;
using CommentService.Domain.Interfaces;

namespace CommentService.Application.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IRepository<Comment> _repository;
        private readonly IMapper _mapper;

        public CommentsService(IRepository<Comment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Comment> Add(CreateCommentDTO commentDto, UserContext userContext)
        {
            var comment = _mapper.Map<Comment>(commentDto);

            comment.UserId = userContext.UserId;
            comment.CommentType = CommentType.Comment;

            Comment addedComment = await _repository.Add(comment);

            return addedComment;
        }

        public async Task Delete(Guid id)
        {
            var comment = await _repository.GetBy(c => c.Id == id);
            if (comment is not null)
            {
                await _repository.Delete(comment);
            }
        }

        public async Task<IEnumerable<CommentDTO>> GetAll()
        {
            var comments = await _repository.GetAllWhere(c => c.CommentType == CommentType.Comment);
            return _mapper.Map<IEnumerable<CommentDTO>>(comments);
        }

        public async Task<IEnumerable<CommentDTO>> GetAllByUserId(Guid userId)
        {
            var comments = await _repository.GetAllWhere(c => c.UserId == userId);
            return _mapper.Map<IEnumerable<CommentDTO>>(comments);
        }

        public async Task<CommentDTO> GetById(Guid id)
        {
            var comment = await _repository.GetBy(c => c.Id == id);
            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<Comment> ReplyComment(Guid commentId, CreateCommentDTO replyCommentDTO, UserContext userContext)
        {
            var comment = await _repository.GetBy(c => c.Id == commentId);

            if (comment is null) throw new Exception("Comment not found.");

            var reply = _mapper.Map<Comment>(replyCommentDTO);
            
            reply.UserId = userContext.UserId;
            comment.Replies.Add(reply);
            reply.CommentType = CommentType.Reply;

            var createdReply = await _repository.Add(reply);


            return createdReply;
        }

        public async Task Update(Guid commentId, CreateCommentDTO commentDto, UserContext userContext)
        {
            var comment = await _repository.GetBy(c => c.Id == commentId);

            comment.Content = commentDto.Content;

            await _repository.Update(comment);
        }

        public async Task VoteComment(Guid commentId, VoteCommentDTO voteDto, UserContext userContext)
        {
            var comment = await _repository.GetBy(c => c.Id == commentId);

            if (comment is null) throw new Exception("Comment not found.");

            var userVote = comment.Votes.FirstOrDefault(v => v.UserId.Equals(userContext.UserId));

            if (userVote is not null)
            {
                if (userVote.Vote == voteDto.Vote)
                {
                    comment.Votes.Remove(userVote);
                    comment.Score += (userVote.Vote == VoteType.UpVote) ? -1 : 1;
                }
                else
                {
                    comment.Score += (userVote.Vote == VoteType.UpVote) ? -1 : 1;
                    userVote.Vote = voteDto.Vote;
                }
            }
            else
            {
                userVote = new CommentVote() { CommentId = commentId, UserId = userContext.UserId, Vote = voteDto.Vote };
                comment.Votes.Add(userVote);
                comment.Score += (voteDto.Vote == VoteType.UpVote) ? 1 : -1;
            }

            await _repository.Update(comment);
        }


        public async Task<bool> HasUserVoted(Guid commentId, UserContext userContext)
        {
            var comment = await _repository.GetBy(c => c.Id == commentId);

            var containsUserVote = comment.Votes.Any(v => v.UserId == userContext.UserId);

            return containsUserVote;
        }

    }
}
