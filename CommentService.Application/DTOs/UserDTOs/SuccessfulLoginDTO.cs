namespace CommentService.Domain.Entities
{
    public record SuccessfulLoginDTO
    {
        public string UserName { get; init; } = string.Empty;
        public string Image { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
