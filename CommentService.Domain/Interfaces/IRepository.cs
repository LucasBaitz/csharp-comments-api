using System.Linq.Expressions;

namespace CommentService.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetBy(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>?> GetAllWhere(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
        Task Delete(T entity);
        Task<T> Update(T entityUpdated);
        Task<bool> SaveChanges();
    }
}
