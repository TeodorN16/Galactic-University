using GalacticUniversity.Core.CertificateService;
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
    public class CertificateServiceTests
    {
        private Mock<IRepository<Certificate>> _mockRepo;
        private CertificateService _service;

        [SetUp]
        public void SetUp()
        {
            _mockRepo = new Mock<IRepository<Certificate>>();
            _service = new CertificateService(_mockRepo.Object);
        }

        [Test]
        public async Task Add_Should_Call_Repo_Add()
        {
            var certificate = new Certificate { CertificateID = 1 };

            await _service.Add(certificate);

            _mockRepo.Verify(r => r.Add(certificate), Times.Once);
        }

        [Test]
        public async Task Delete_Should_Call_Repo_Delete()
        {
            var certificate = new Certificate { CertificateID = 1 };

            await _service.Delete(certificate);

            _mockRepo.Verify(r => r.Delete(certificate), Times.Once);
        }

        [Test]
        public async Task Get_Should_Return_Correct_Certificate()
        {
            var expected = new Certificate { CertificateID = 1 };
            _mockRepo.Setup(r => r.Get(1)).ReturnsAsync(expected);

            var result = await _service.Get(1);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAll_Should_Return_All_Certificates()
        {
            var data = new List<Certificate>
            {
                new Certificate { CertificateID = 1 },
                new Certificate { CertificateID = 2 }
            }.AsQueryable();

            _mockRepo.Setup(r => r.GetAll()).Returns(data);

            var result = _service.GetAll();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task Find_Should_Return_Filtered_Certificates()
        {
            var expected = new List<Certificate>
            {
                new Certificate { CertificateID = 1, UserID = "user1" }
            };

            Expression<Func<Certificate, bool>> filter = c => c.UserID == "user1";

            _mockRepo.Setup(r => r.Find(filter)).ReturnsAsync(expected);

            var result = await _service.Find(filter);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("user1", result[0].UserID);
        }

        [Test]
        public async Task GetCertificatesByUser_Should_Return_Certificates_For_User()
        {
            var userId = "user123";
            var expected = new List<Certificate>
            {
                new Certificate { CertificateID = 1, UserID = userId },
                new Certificate { CertificateID = 2, UserID = userId }
            };

            _mockRepo.Setup(r => r.Find(It.IsAny<Expression<Func<Certificate, bool>>>()))
                .ReturnsAsync(expected);

            var result = await _service.GetCertificatesByUser(userId);

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(c => c.UserID == userId));
        }

        [Test]
        public async Task Update_Should_Call_Repo_Update()
        {
            var certificate = new Certificate { CertificateID = 1 };

            await _service.Update(certificate);

            _mockRepo.Verify(r => r.Update(certificate), Times.Once);
        }
    }
}
