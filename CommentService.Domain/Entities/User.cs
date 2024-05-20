using Microsoft.AspNetCore.Identity;

namespace CommentService.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Image { get; set; } = string.Empty;
        public virtual ICollection<Comment> Comments { get; set; } = null!;
    }
}
