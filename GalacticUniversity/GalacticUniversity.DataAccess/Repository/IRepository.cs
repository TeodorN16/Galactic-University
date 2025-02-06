using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.Core.Repository
{
    public interface IRepository<T> where T:class
    {
        public  Task Add(T obj);

        public Task Delete(T obj);

        public Task Update(T obj);
        
        public IQueryable<T> GetAll();

        public Task<T> Get(int id);

        public Task<List<T>> Find(Expression<Func<T, bool>>filter);
        
    }
}
