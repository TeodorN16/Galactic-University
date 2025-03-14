// Core/CertificateService/CertificateService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Models;

namespace GalacticUniversity.Core.CertificateService
{
    public class CertificateService : ICertificateService
    {
        private readonly IRepository<Certificate> repo;

        public CertificateService(IRepository<Certificate> _repo)
        {
            repo = _repo;
        }

        public async Task Add(Certificate obj)
        {
            await repo.Add(obj);
        }

        public async Task Delete(Certificate obj)
        {
            await repo.Delete(obj);
        }

        public async Task<Certificate> Get(int id)
        {
            Certificate certificate = await repo.Get(id);
            return certificate;
        }

        public IQueryable<Certificate> GetAll()
        {
            return repo.GetAll();
        }

        public async Task<List<Certificate>> Find(Expression<Func<Certificate, bool>> filter)
        {
            return await repo.Find(filter);
        }

        public async Task<List<Certificate>> GetCertificatesByUser(string userId)
        {
            Expression<Func<Certificate, bool>> filter = certificate => certificate.UserID == userId;
            return await repo.Find(filter);
        }

        public async Task Update(Certificate obj)
        {
            await repo.Update(obj);
        }
    }
}