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

        public Service(IRepository<T> repository)
        {
            repo = repository;
        }

        public async Task Add(T obj)
        {
            await repo.Add(obj);
        }

        public async Task Delete(T obj)
        {
           await repo.Delete(obj);
        }

        public async Task<T> Get(int id)
        {
            T obj = await repo.Get(id);
            return obj;
        }

        public  IQueryable<T> GetAll()
        {
            return repo.GetAll();
        }

        public async Task Update(T obj)
        {
           await repo.Update(obj);
        }
    }
}
