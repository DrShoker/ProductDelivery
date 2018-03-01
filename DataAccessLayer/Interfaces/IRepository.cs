using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<T> 
        where T : class  
    {
        IEnumerable<T> GetAll();

        T Get(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        T FirstOrDefault(Func<T, bool> predicate);
        Task<T> FirstOrDefaultAsync(Func<T, bool> predicate);
    }
}
