using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using ReportSummary.Api.Controllers;
using ReportSummary.Configuration;
using ReportSummary.Services;
using System;
using System.Threading.Tasks;

namespace ReportSummary.Api.Tests.Controllers
{
    [TestFixture]
    public class ReportControllerTests
    {
        private MockRepository mockRepository;

        private Mock<IReportService> mockReportService;
        private Mock<IOptions<CosmosConfiguration>> mockOptions;

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockReportService = this.mockRepository.Create<IReportService>();
            this.mockOptions = this.mockRepository.Create<IOptions<CosmosConfiguration>>();
        }

        private ReportController CreateReportController()
        {
            return new ReportController(
                this.mockReportService.Object,
                this.mockOptions.Object);
        }

        [Test]
        public void Get_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var reportController = this.CreateReportController();

            // Act
            var result = reportController.Get();

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public async Task Get_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var reportController = this.CreateReportController();
            Guid id = default(global::System.Guid);

            // Act
            var result = await reportController.Get(
                id);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Post_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var reportController = this.CreateReportController();
            string value = null;

            // Act
            reportController.Post(
                value);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Put_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var reportController = this.CreateReportController();
            int id = 0;
            string value = null;

            // Act
            reportController.Put(
                id,
                value);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Delete_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var reportController = this.CreateReportController();
            int id = 0;

            // Act
            reportController.Delete(
                id);

            // Assert
            Assert.Fail();
            this.mockRepository.VerifyAll();
        }
    }
}
