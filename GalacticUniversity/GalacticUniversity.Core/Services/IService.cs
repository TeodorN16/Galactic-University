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

        public IQueryable<T> GetAll();

        

        public T Get(int id);
    }
}
