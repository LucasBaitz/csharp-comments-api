using System.Text;

namespace CommentService.Infrastructure.Security.Token
{
    public class JwtSettings
    {
        public bool ValidateIssuerSigningKey { get; set; }
        public string IssuerSigningKey { get; set; } = string.Empty;
        public bool ValidateLifetime { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateIssuer { get; set; }
        public TimeSpan ClockSkew { get; set; }
    }
}
