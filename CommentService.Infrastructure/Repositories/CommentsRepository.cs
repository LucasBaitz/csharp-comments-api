using CommentService.Domain.Entities;
using CommentService.Domain.Interfaces;
using CommentService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CommentService.Infrastructure.Repositories
{
    public class CommentsRepository : IRepository<Comment>
    {
        private readonly AppDbContext _context;

        public CommentsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> Add(Comment entity)
        {
            var createdEntity = await _context.Comments.AddAsync(entity);
            await SaveChangesAsync();

            return createdEntity.Entity;
        }

        public async Task Delete(Comment entity)
        {
            _context.Comments.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllWhere(Expression<Func<Comment, bool>> predicate)
        {
            return await _context.Comments.Where(predicate).ToListAsync();
        }

        public async Task<Comment?> GetBy(Expression<Func<Comment, bool>> predicate)
        {
            return await _context.Comments.FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<Comment> Update(Comment entityUpdated)
        {
            _context.Entry(entityUpdated).State = EntityState.Modified;
            await SaveChangesAsync();
            return entityUpdated;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
