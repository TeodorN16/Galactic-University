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
        public void Add(T obj);

        public void Delete(T obj);

        public void Update(T obj);
        
        public IQueryable<T> GetAll();

        public T Get(int id);

        public List<T> Find(Expression<Func<T, bool>>filter);
        
    }
}
