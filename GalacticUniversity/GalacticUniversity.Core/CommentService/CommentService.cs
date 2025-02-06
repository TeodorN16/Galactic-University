using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Models;

namespace GalacticUniversity.Core.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> repo;
        public CommentService(IRepository<Comment> _repo)
        {
             repo= _repo;
        }
        public async Task Add(Comment obj)
        {
          await  repo.Add(obj);
        }

        public async Task Delete(Comment obj)
        {
           await repo.Delete(obj);
        }

        

       

        public async Task<Comment> Get(int id)
        {
           Comment comment = await repo.Get(id);
            return comment;
        }

        public  IQueryable<Comment> GetAll()
        {
            return  repo.GetAll();
        }

        public async Task Update(Comment obj)
        {
            await repo.Update(obj);
        }
    }
}
