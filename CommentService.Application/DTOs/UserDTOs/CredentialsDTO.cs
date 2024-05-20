using System.ComponentModel.DataAnnotations;

namespace CommentService.Application.DTOs.UserDTOs
{
    public record CredentialsDTO
    {
        [Required]
        public string UserName { get; init; } = string.Empty;
        [Required]
        public string Password { get; init; } = string.Empty;
    }
}
