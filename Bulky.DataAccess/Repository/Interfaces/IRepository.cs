using System.Linq.Expressions;


namespace Bulky.DataAccess.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // T - Generic model of table in database with which we want to interact with or conduct CRUD operations
        IEnumerable<T> GetAll(string? includeProperties = null);

        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);

        void Add(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

    }
}
