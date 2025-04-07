using GalacticUniversity.Core.CourseService;
using GalacticUniversity.Core.Repository;
using GalacticUniversity.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GalacticUniversity.Tests
{
    [TestFixture]
    public class CourseServiceTests
    {
        private Mock<IRepository<Course>> mockRepo;
        private CourseService service;

        [SetUp]
        public void Setup()
        {
            mockRepo = new Mock<IRepository<Course>>();
            service = new CourseService(mockRepo.Object);
        }

        [Test]
        public async Task Add_Should_Call_Repo_Add()
        {
            var course = new Course { CourseID = 1, CourseName = "Test Course" };

            await service.Add(course);

            mockRepo.Verify(r => r.Add(course), Times.Once);
        }

        [Test]
        public async Task Delete_Should_Call_Repo_Delete()
        {
            var course = new Course { CourseID = 1, CourseName = "Test Course" };

            await service.Delete(course);

            mockRepo.Verify(r => r.Delete(course), Times.Once);
        }

        [Test]
        public async Task Get_Should_Return_Correct_Course()
        {
            var course = new Course { CourseID = 1, CourseName = "Test Course" };
            mockRepo.Setup(r => r.Get(1)).ReturnsAsync(course);

            var result = await service.Get(1);

            Assert.AreEqual(course, result);
        }

        [Test]
        public void GetAll_Should_Return_All_Courses()
        {
            var courses = new List<Course>
            {
                new Course { CourseID = 1, CourseName = "Course 1" },
                new Course { CourseID = 2, CourseName = "Course 2" }
            }.AsQueryable();

            mockRepo.Setup(r => r.GetAll()).Returns(courses);

            var result = service.GetAll();

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(c => c.CourseName == "Course 1"));
        }

        [Test]
        public async Task GetCourseByCategory_Should_Return_Filtered_Courses()
        {
            var category = new Category { CategoryID = 1, CategoryName = "Physics" };
            var courses = new List<Course>
            {
                new Course { CourseID = 1, CourseName = "Quantum Mechanics", Category = category },
                new Course { CourseID = 2, CourseName = "Astrophysics", Category = category }
            };

            mockRepo.Setup(r => r.Find(It.IsAny<Expression<Func<Course, bool>>>()))
                    .ReturnsAsync((Expression<Func<Course, bool>> filter) =>
                        courses.Where(filter.Compile()).ToList());

            var result = await service.GetCourseByCategory("Physics");

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(c => c.Category.CategoryName == "Physics"));
        }

        [Test]
        public async Task Update_Should_Call_Repo_Update()
        {
            var course = new Course { CourseID = 1, CourseName = "Updated Course" };

            await service.Update(course);

            mockRepo.Verify(r => r.Update(course), Times.Once);
        }
    }
}
