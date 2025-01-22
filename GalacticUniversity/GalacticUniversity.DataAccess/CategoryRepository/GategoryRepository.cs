using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Models;

namespace GalacticUniversity.DataAccess.CategoryRepository
{
    public class GategoryRepository:Repository<Category>
    {
        private readonly ApplicationDbContext _context;
        public GategoryRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

    }
}
