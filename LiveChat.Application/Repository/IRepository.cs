using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Repository
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        T? Get(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll();
        IEnumerable<T> Where(Expression<Func<T, bool>> filter);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
