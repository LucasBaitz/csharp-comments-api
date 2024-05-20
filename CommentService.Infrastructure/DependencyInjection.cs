using CommentService.Application.Interfaces.Security;
using CommentService.Domain.Entities;
using CommentService.Domain.Interfaces;
using CommentService.Infrastructure.Persistence;
using CommentService.Infrastructure.Repositories;
using CommentService.Infrastructure.Security;
using CommentService.Infrastructure.Security.Token;
using CommentService.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CommentService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettingsSection = configuration.GetSection("Auth");
            services.Configure<JwtSettings>(jwtSettingsSection);
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

            services.AddIdentity<User, IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = jwtSettings!.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey)),
                    ValidateLifetime = jwtSettings.ValidateLifetime,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ClockSkew = jwtSettings.ClockSkew
                };
            });

            var connectionString = configuration.GetConnectionString("SqliteConnectionString");

            services.AddDbContext<AppDbContext>(opts =>
            {
                opts.UseLazyLoadingProxies().UseSqlite(connectionString,
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
            });

            services.AddScoped<IRepository<Comment>, CommentsRepository>();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserContextProvider, UserContextProvider>();

            return services;
        }
    }
}
