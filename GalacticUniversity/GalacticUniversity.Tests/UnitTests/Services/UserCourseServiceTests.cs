using GalacticUniversity.Core.UserCourseService;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticUniversity.Tests
{
    [TestFixture]
    public class UserCourseServiceTests
    {
        private Mock<IRepository<UserCourses>> _mockUserCourseRepo;
        private Mock<IRepository<Course>> _mockCourseRepo;
        private UserCourseService _service;

        [SetUp]
        public void Setup()
        {
            _mockUserCourseRepo = new Mock<IRepository<UserCourses>>();
            _mockCourseRepo = new Mock<IRepository<Course>>();
            _service = new UserCourseService(_mockUserCourseRepo.Object, _mockCourseRepo.Object);
        }

        [Test]
        public async Task Add_Should_Call_Repo_Add()
        {
            var userCourse = new UserCourses { UserID = "user1", CourseID = 1 };

            await _service.Add(userCourse);

            _mockUserCourseRepo.Verify(r => r.Add(userCourse), Times.Once);
        }

        [Test]
        public async Task Delete_Should_Call_Repo_Delete()
        {
            var userCourse = new UserCourses { UserID = "user1", CourseID = 1 };

            await _service.Delete(userCourse);

            _mockUserCourseRepo.Verify(r => r.Delete(userCourse), Times.Once);
        }

        [Test]
        public async Task Get_Should_Return_UserCourse_By_Id()
        {
            var expected = new UserCourses { Id = 1 };
            _mockUserCourseRepo.Setup(r => r.Get(1)).ReturnsAsync(expected);

            var result = await _service.Get(1);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAll_Should_Return_All_UserCourses()
        {
            var data = new List<UserCourses>
            {
                new UserCourses { Id = 1 },
                new UserCourses { Id = 2 }
            }.AsQueryable();

            _mockUserCourseRepo.Setup(r => r.GetAll()).Returns(data);

            var result = _service.GetAll();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task JoinCourse_Should_Return_False_If_User_Already_Joined()
        {
            var userCourses = new List<UserCourses>
            {
                new UserCourses { UserID = "user1", CourseID = 1 }
            }.AsQueryable();

            _mockUserCourseRepo.Setup(r => r.GetAll()).Returns(userCourses);

            var result = await _service.JoinCourse("user1", 1);

            Assert.IsFalse(result);
            _mockUserCourseRepo.Verify(r => r.Add(It.IsAny<UserCourses>()), Times.Never);
        }

        [Test]
        public async Task JoinCourse_Should_Add_And_Return_True_If_Not_Joined()
        {
            _mockUserCourseRepo.Setup(r => r.GetAll()).Returns(new List<UserCourses>().AsQueryable());

            var result = await _service.JoinCourse("user1", 1);

            Assert.IsTrue(result);
            _mockUserCourseRepo.Verify(r => r.Add(It.Is<UserCourses>(uc => uc.UserID == "user1" && uc.CourseID == 1)), Times.Once);
        }

        [Test]
        public async Task LeaveCourse_Should_Return_False_If_Course_Not_Exists()
        {
            _mockCourseRepo.Setup(r => r.GetAll()).Returns(new List<Course>().AsQueryable());

            var result = await _service.LeaveCourse("user1", 1);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task LeaveCourse_Should_Return_False_If_User_Not_Enrolled()
        {
            var courses = new List<Course> { new Course { CourseID = 1 } }.AsQueryable();
            var userCourses = new List<UserCourses>().AsQueryable();

            _mockCourseRepo.Setup(r => r.GetAll()).Returns(courses);
            _mockUserCourseRepo.Setup(r => r.GetAll()).Returns(userCourses);

            var result = await _service.LeaveCourse("user1", 1);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task LeaveCourse_Should_Delete_And_Return_True_If_Valid()
        {
            var courses = new List<Course> { new Course { CourseID = 1 } }.AsQueryable();
            var userCourse = new UserCourses { UserID = "user1", CourseID = 1 };
            var userCourses = new List<UserCourses> { userCourse }.AsQueryable();

            _mockCourseRepo.Setup(r => r.GetAll()).Returns(courses);
            _mockUserCourseRepo.Setup(r => r.GetAll()).Returns(userCourses);

            var result = await _service.LeaveCourse("user1", 1);

            Assert.IsTrue(result);
            _mockUserCourseRepo.Verify(r => r.Delete(userCourse), Times.Once);
        }

        [Test]
        public async Task Update_Should_Call_Repo_Update()
        {
            var userCourse = new UserCourses { Id = 1 };

            await _service.Update(userCourse);

            _mockUserCourseRepo.Verify(r => r.Update(userCourse), Times.Once);
        }
    }
}
