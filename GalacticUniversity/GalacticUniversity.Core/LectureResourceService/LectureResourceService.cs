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
        public async Task Add(LectureResource obj)
        {
           await repo.Add(obj);
        }

        public async Task Delete(LectureResource obj)
        {
            await repo.Delete(obj);
        }

        public async Task<LectureResource> Get(int id)
        {
            LectureResource lectureResource = await repo.Get(id);
            return lectureResource;
        }

        public  IQueryable<LectureResource> GetAll()
        {
            return  repo.GetAll();
        }

       

        public async Task Update(LectureResource obj)
        {
             await repo.Update(obj);
        }

       
    }
}
