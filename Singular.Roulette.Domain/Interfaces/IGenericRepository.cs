using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(long id);

        Task<T> Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
