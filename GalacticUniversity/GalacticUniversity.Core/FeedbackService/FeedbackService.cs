using GalacticUniversity.Core.Repository;
using GalacticUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.Core.FeedbackService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IRepository<Feedback> _repo;

        public FeedbackService(IRepository<Feedback>repo)
        { 
            _repo = repo;
        }
        public async Task Add(Feedback obj)
        {
            await _repo.Add(obj);
        }

        public async Task Delete(Feedback obj)
        {
            await _repo.Delete(obj);
        }

        public async Task<Feedback> Get(int id)
        {
            Feedback Feedback = await _repo.Get(id);
            return Feedback;
        }

        public IQueryable<Feedback> GetAll()
        {
            return _repo.GetAll();
        }


        public async Task Update(Feedback obj)
        {
            await _repo.Update(obj);
        }
    }
}
