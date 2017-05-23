using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Abstract
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T,bool> predicate);
        T Create(T item);
        void Update(T item);
        T Delete(T item);
    }
}
