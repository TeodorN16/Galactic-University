using GalacticUniversity.Core.Repository;
using GalacticUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.DataAccess.LectureResourceRepository
{
    public class LectureResourceRepository : Repository<LectureResource>
    {
      
            private readonly ApplicationDbContext _context;

            public LectureResourceRepository(ApplicationDbContext context) : base(context)
            {
                _context = context;
            }

           
        
    }
}
