using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.Core.Repository
{
    public interface IRepository<T> where T:class
    {
        public void Add(T obj);

        public void Delete(T obj);

        public void Update(T obj);

        public List<T> GetAll();
        
    }
}
