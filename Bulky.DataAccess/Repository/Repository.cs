using Bulky.DataAccess.Repository.Interfaces;
using Bulky.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.ComponentModel.Design;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        // includeProperties will be comma separated string to include FK related model properties
        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }
            
            query = query.Where(filter);
            // Include FK related model data
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includePoperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includePoperty);
                }
            }
            return query.FirstOrDefault();

        }

            // includeProperties will be comma separated string to include FK related model properties
            public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter!=null )
            {
                query = query.Where(filter);
            }

            // Include FK related model data
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includePoperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includePoperty);
                }
            }
            return query.ToList();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
