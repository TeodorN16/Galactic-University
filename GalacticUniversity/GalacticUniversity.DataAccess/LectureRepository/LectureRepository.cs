using GalacticUniversity.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.DataAccess.LectureRepository
{
    public class LectureRepository:Repository<LectureRepository>
    {
        private readonly ApplicationDbContext _context;
        public LectureRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
