using CommentService.Domain.Entities;
using CommentService.Application.Interfaces.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;

namespace CommentService.Infrastructure.Security.Token
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly SymmetricSecurityKey _securityKey;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtSettingsOptions)
        {
            var jwtSettings = jwtSettingsOptions.Value;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey));

        }

        public string GenerateToken(User user)
        {

            List<Claim> claims = new()
            {
                new("id", user.Id.ToString()),
                new("userName", user.UserName!),
                new("email", user.Email!),
                new("loginTimeStamp", DateTime.UtcNow.ToString())
            };

            var signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "CommentApi",
                expires: DateTime.Now.AddHours(24),
                claims: claims,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
