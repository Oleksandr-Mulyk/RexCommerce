using System.ComponentModel;
using System.Linq.Expressions;

namespace RexCommerce.RepositoryLibrary
{
    public interface IRepository<T, TId> where T : class
    {
        IQueryable<T> GetAll();

        IQueryable<T> GetAll(IEnumerable<Expression<Func<T, bool>>> filterExpressions);

        IQueryable<T> GetAll(
            Expression<Func<T, string?>> sortExpression,
            ListSortDirection sortDirection = ListSortDirection.Ascending
            );

        IQueryable<T> GetAll(
            IEnumerable<Expression<Func<T, bool>>> filterExpressions,
            Expression<Func<T, string?>> sortExpression,
            ListSortDirection sortDirection = ListSortDirection.Ascending
            );

        Task<T> GetByIdAsync(TId id);

        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }

    public interface IRepository<T> : IRepository<T, Guid> where T : class
    {
    }
}
