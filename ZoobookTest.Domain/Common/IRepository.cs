using System.Linq.Expressions;

namespace ZoobookTest.Domain.Common
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
