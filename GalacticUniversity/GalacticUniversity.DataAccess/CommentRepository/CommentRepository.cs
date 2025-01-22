using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Models;

namespace GalacticUniversity.DataAccess.CommentRepository
{
    public class CommentRepository:Repository<Comment>
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
    }
}
