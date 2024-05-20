using CommentService.Application.DTOs.Comment;
using CommentService.Application.Interfaces;
using CommentService.Application.Interfaces.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommentService.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public sealed class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;
        private readonly IUserContextProvider _userContextProvider;

        public CommentsController(ICommentsService commentsService, IUserContextProvider userContextProvider)
        {
            _commentsService = commentsService;
            _userContextProvider = userContextProvider;
        }

        [HttpGet]
        [Route("All")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentsService.GetAll();
            return Ok(comments);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var comment = await _commentsService.GetById(id);
            return Ok(comment);
        }

        [HttpGet]
        [Route("/Like/{commentId}")]
        public async Task<IActionResult> HasUserVoted(Guid commentId)
        {
            var userContext = await _userContextProvider.GetContext(this.User);

            var result = _commentsService.HasUserVoted(commentId, userContext);
            return Ok(result);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Create(CreateCommentDTO commentDto)
        {
            var userContext = await _userContextProvider.GetContext(this.User);

            var createdComment = await _commentsService.Add(commentDto, userContext);

            return CreatedAtAction(nameof(GetById), new { id = createdComment.Id }, createdComment);
        }

        [HttpPost]
        [Route("Reply/{commentId}")]
        public async Task<IActionResult> ReplyComment(Guid commentId, CreateCommentDTO replyDto)
        {
            var userContext = await _userContextProvider.GetContext(this.User);

            var createdReply = await _commentsService.ReplyComment(commentId, replyDto, userContext);

            var entity = await _commentsService.GetById(createdReply.Id);

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut]
        [Route("Update/{commentId}")]
        public async Task<IActionResult> UpdateComment(Guid commentId, CreateCommentDTO commentDto)
        {
            var userContext = await _userContextProvider.GetContext(this.User);

            await _commentsService.Update(commentId, commentDto, userContext);

            return NoContent();
        }

        [HttpPost]
        [Route("Vote/{commentId}")]
        public async Task<IActionResult> VoteOnComment(Guid commentId, VoteCommentDTO voteDto)
        {
            var userContext = await _userContextProvider.GetContext(this.User);

            await _commentsService.VoteComment(commentId, voteDto, userContext);

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete/{commentId}")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            var userContext = await _userContextProvider.GetContext(this.User);

            await _commentsService.Delete(commentId);

            return NoContent();
        }
    }
}


