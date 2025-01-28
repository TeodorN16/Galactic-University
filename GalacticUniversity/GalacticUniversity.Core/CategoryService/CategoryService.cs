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
        public void Add(Category obj)
        {
          repo.Add(obj);
        }

        public void Delete(Category obj)
        {
            repo.Delete(obj);   
        }

        public Category Get(int id)
        {
            Category category = repo.Get(id);
            return category;
        }

        public List<Category> GetAll()
        {
            return repo.GetAll();
        }

        public void Update(Category obj)
        {
            repo.Update(obj);
        }
    }
}
