using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Models;

namespace GalacticUniversity.Core.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> repo;

        public CategoryService(IRepository<Category>_repo)
        {
            repo = _repo;
        }
        public async Task Add(Category obj)
        {
          await repo.Add(obj);
        }

        public async Task Delete(Category obj)
        {
            await repo.Delete(obj);   
        }

        public async Task<Category> Get(int id)
        {
            Category category = await repo.Get(id);
            return category;
        }

        public  IQueryable<Category> GetAll()
        {
            return  repo.GetAll();
        }

        public async Task Update(Category obj)
        {
            await repo.Update(obj);
        }
    }
}
