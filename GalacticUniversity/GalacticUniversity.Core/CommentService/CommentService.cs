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
        public void Add(Comment obj)
        {
            repo.Add(obj);
        }

        public void Delete(Comment obj)
        {
            repo.Delete(obj);
        }

        public List<Comment> FilterCommentsByDate()
        {
            return repo.GetAll()
                    .OrderBy(x=>x.CommentDate)
                    .ToList();
        }

        public List<Comment> FilterCommentsByRating()
        {
            throw new Exception();
        }

        public Comment Get(int id)
        {
           Comment comment = repo.Get(id);
            return comment;
        }

        public List<Comment> GetAll()
        {
            return repo.GetAll();
        }

        public void Update(Comment obj)
        {
            repo.Update(obj);
        }
    }
}
