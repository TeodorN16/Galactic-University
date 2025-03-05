using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GalacticUniversity.DataAccess.UserRepository
{
    public class UserRepository<T> : IUserRepository<T> where T:class
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> dbset;

        public UserRepository(ApplicationDbContext _context)
        {
            context = _context;
            dbset = context.Set<T>();
        }
        public async Task Add(T obj)
        {
            await context.AddAsync(obj);
            await context.SaveChangesAsync();
        }

        public async Task Delete(T obj)
        {

            context.Remove(obj);
            await context.SaveChangesAsync();

        }

        public IQueryable<T> GetAll()
        {
            return dbset.AsQueryable();
        }

        public async Task Update(T obj)
        {
            context.Update(obj);
            await context.SaveChangesAsync();
        }

        public async Task<T> Get(string id)
        {
            T obj = await dbset.FindAsync(id);
            return obj;
        }
        public async Task<List<T>> Find(Expression<Func<T, bool>> filter)
        {
            return await dbset.Where(filter).ToListAsync();
        }
    }
}
