
using LiveChat.Application.Repository;
using LiveChat.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LiveChat.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public DbSet<T> Entities { get; set; } 

        private readonly LiveChatDbContext _dbContext;

        public Repository(LiveChatDbContext dbContext)
        {
            _dbContext = dbContext;
            Entities = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            Entities.Add(entity);
        }

        public T? Get(Expression<Func<T, bool>> filter)
        {
            return Entities.Where(filter).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return Entities;
        }

        public void Remove(T entity)
        {
            Entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Entities.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            Entities.Update(entity);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> filter)
        {
            return Entities.Where(filter);
        }
    }
}
