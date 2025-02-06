using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Core.Services;
using GalacticUniversity.Models;

namespace GalacticUniversity.Core.LectureService
{
    public class LectureService : ILectureService
    {
        private readonly IRepository<Lecture> repo;
        public LectureService(IRepository<Lecture> _repo)
        {
            this.repo = _repo;       
        }
        public async Task Add(Lecture obj)
        {
            await repo.Add(obj);
        }

        public async Task Delete(Lecture obj)
        {
            await repo.Delete(obj);
        }

        public async Task<Lecture> Get(int id)
        {
            Lecture lecture = await repo.Get(id);
            return lecture;
        }

        public  IQueryable<Lecture> GetAll()
        {
            return  repo.GetAll();
        }

        public async Task Update(Lecture obj)
        {
            await repo.Update(obj);
        }
    }
}
