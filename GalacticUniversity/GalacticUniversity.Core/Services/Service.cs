using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.DataAccess;


namespace GalacticUniversity.Core.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IRepository<T> repo;
        public void Add(T obj)
        {
           repo.Add(obj);
        }

        public void Delete(T obj)
        {
            repo.Delete(obj);
        }

        public T Get(int id)
        {
            T obj = repo.Get(id);
            return obj;
        }

        public List<T> GetAll()
        {
           return repo.GetAll();
        }

        public void Update(T obj)
        {
          repo.Update(obj);
        }
    }
}
