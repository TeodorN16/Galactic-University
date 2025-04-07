using GalacticUniversity.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace GalacticUniversity.Tests.Mock
{
    public class DbMock
    {
        public static ApplicationDbContext Instance
        {
            get
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("GalacticUniversityTestDb_" + Guid.NewGuid())
                    .Options;

                return new ApplicationDbContext(options);
           }
       }
    }
}
