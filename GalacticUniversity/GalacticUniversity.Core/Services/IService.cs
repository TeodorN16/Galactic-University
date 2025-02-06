using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.Core.Services
{
    public interface IService<T> where T : class
    {
         
        public Task Add(T obj);

        public Task Delete(T obj);

        public Task Update(T obj);

        public IQueryable<T> GetAll();

        public Task<T> Get(int id);
    }
}
