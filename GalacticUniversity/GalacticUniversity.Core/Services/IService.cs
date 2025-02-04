using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.Core.Services
{
    public interface IService<T> where T : class
    {
         
        public void Add(T obj);

        public void Delete(T obj);

        public void Update(T obj);

        public List<T> GetAll();

        public IQueryable<T> GetAll1();

        public T Get(int id);
    }
}
