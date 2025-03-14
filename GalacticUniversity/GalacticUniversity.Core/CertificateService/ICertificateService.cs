using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GalacticUniversity.Models;

namespace GalacticUniversity.Core.CertificateService
{
    public interface ICertificateService
    {
        Task Add(Certificate obj);
        Task Delete(Certificate obj);
        Task Update(Certificate obj);

        IQueryable<Certificate> GetAll();
        Task<Certificate> Get(int id);
        Task<List<Certificate>> Find(Expression<Func<Certificate, bool>> filter);
        Task<List<Certificate>> GetCertificatesByUser(string userId);
    }
}
