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
        public void Add(Lecture obj)
        {
            repo.Add(obj);
        }

        public void Delete(Lecture obj)
        {
            repo.Delete(obj);
        }

        public Lecture Get(int id)
        {
            Lecture lecture = repo.Get(id);
            return lecture;
        }

        public IQueryable<Lecture> GetAll()
        {
            return repo.GetAll();
        }

        public void Update(Lecture obj)
        {
            repo.Update(obj);
        }
    }
}
