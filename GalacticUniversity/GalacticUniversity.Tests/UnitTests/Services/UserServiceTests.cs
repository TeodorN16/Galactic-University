using GalacticUniversity.Core.UserService;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.DataAccess.UserRepository;
using GalacticUniversity.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticUniversity.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository<User>> mockUserRepo;
        private Mock<UserManager<User>> mockUserManager;
        private UserService service;

        [SetUp]
        public void Setup()
        {
            mockUserRepo = new Mock<IUserRepository<User>>();

            // Mock UserManager with default setup
            var store = new Mock<IUserStore<User>>();
            mockUserManager = new Mock<UserManager<User>>(
                store.Object, null, null, null, null, null, null, null, null);

            service = new UserService(mockUserManager.Object, mockUserRepo.Object);
        }

        [Test]
        public async Task Add_Should_Call_Repo_Add()
        {
            var user = new User { UserName = "testuser" };

            await service.Add(user);

            mockUserRepo.Verify(r => r.Add(user), Times.Once);
        }

        [Test]
        public async Task Delete_Should_Call_Repo_Delete()
        {
            var user = new User { UserName = "testuser" };

            await service.Delete(user);

            mockUserRepo.Verify(r => r.Delete(user), Times.Once);
        }

        [Test]
        public async Task Get_Should_Return_User_By_Id()
        {
            var user = new User { Id = "123", UserName = "testuser" };
            mockUserRepo.Setup(r => r.Get("123")).ReturnsAsync(user);

            var result = await service.Get("123");

            Assert.AreEqual(user, result);
        }

        [Test]
        public void GetAll_Should_Return_All_Users()
        {
            var users = new List<User>
            {
                new User { Id = "1", UserName = "user1" },
                new User { Id = "2", UserName = "user2" }
            }.AsQueryable();

            mockUserRepo.Setup(r => r.GetAll()).Returns(users);

            var result = service.GetAll();

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(u => u.UserName == "user1"));
        }

        [Test]
        public async Task Update_Should_Call_Repo_Update()
        {
            var user = new User { UserName = "updateduser" };

            await service.Update(user);

            mockUserRepo.Verify(r => r.Update(user), Times.Once);
        }
    }
}

