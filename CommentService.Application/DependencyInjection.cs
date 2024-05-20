using CommentService.Application.Interfaces;
using CommentService.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CommentService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ICommentsService, CommentsService>();

            return services;
        }
    }
}
