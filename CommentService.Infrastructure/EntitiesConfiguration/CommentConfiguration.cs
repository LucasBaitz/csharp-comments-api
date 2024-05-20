using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CommentService.Domain.Entities;

namespace CommentService.Infrastructure.EntitiesConfiguration
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(p => p.User);

            builder.HasMany(c => c.Replies)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
