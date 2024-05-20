namespace CommentService.Application.DTOs.UserDTOs
{
    public record AuthorUserDTO
    {
        public string UserName { get; init; } = string.Empty;
        public string Image { get; init; } = string.Empty;
    }
}
