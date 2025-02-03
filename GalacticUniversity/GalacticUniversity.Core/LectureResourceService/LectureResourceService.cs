using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace GalacticUniversity.Core.LectureResourceService
{
    public class LectureResourceService : ILectureResourceService
    {
        private readonly IRepository<LectureResource> repo;
        public LectureResourceService(IRepository<LectureResource>_repo)
        {
            repo = _repo;
        }
        public void Add(LectureResource obj)
        {
           repo.Add(obj);
        }

        public void Delete(LectureResource obj)
        {
            repo.Delete(obj);
        }

        public LectureResource Get(int id)
        {
            LectureResource lectureResource =  repo.Get(id);
            return lectureResource;
        }

        public List<LectureResource> GetAll()
        {
            return repo.GetAll();
        }

        

        public void Update(LectureResource obj)
        {
             repo.Update(obj);
        }

       
    }
}
