using GalacticUniversity.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.Core.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext context;
        private readonly DbSet<T> dbset;

        public Repository(ApplicationDbContext _context)
        {
            context = _context;
            dbset = context.Set<T>();
        }
        public void Add(T obj)
        {
            context.Add(obj);
            context.SaveChanges();
        }

        public void Delete(T obj)
        {
            
            context.Remove(obj);
            context.SaveChanges();

        }

        public IQueryable<T> GetAll()
        {
            return dbset.AsQueryable();
        }

        public void Update(T obj)
        {
            context.Update(obj);
            context.SaveChanges();
        }

        public T Get(int id) 
        { 
            T obj  = dbset.Find(id);
            return obj;
        }
        public List<T> Find(Expression<Func<T,bool>>filter)
        { 
            return dbset.Where(filter).ToList();
        }

       
    }
}
