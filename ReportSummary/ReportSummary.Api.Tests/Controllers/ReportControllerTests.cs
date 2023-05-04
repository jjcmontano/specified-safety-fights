using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using ReportSummary.Api.Controllers;
using ReportSummary.Api.Model;
using ReportSummary.Configuration;
using ReportSummary.Model;
using ReportSummary.Services;
using System;
using System.Threading.Tasks;

namespace ReportSummary.Api.Tests.Controllers
{
    [TestFixture]
    public class ReportControllerTests
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        // These fields will be initialised during SetUp() rather than in the constructor
        private MockRepository mockRepository;

        private Mock<IReportService> mockReportService;
        private Mock<IOptions<CosmosConfiguration>> mockOptions;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockReportService = this.mockRepository.Create<IReportService>();
            
            this.mockOptions = this.mockRepository.Create<IOptions<CosmosConfiguration>>();
            this.mockOptions.SetupGet(s => s.Value).Returns(new CosmosConfiguration
            {
                Container = "Container",
                Database = "Database",
                Endpoint = "Endpoint",
                DefaultPartitionKey = "DefaultPartitionKey",
                PrimaryKey = "PrimaryKey",
            });
        }

        private ReportController CreateReportController()
        {
            return new ReportController(
                this.mockReportService.Object,
                this.mockOptions.Object);
        }

        [Test]
        public void Get_HasListOfReports_ReportsReturned()
        {
            // Arrange
            var reportController = this.CreateReportController();
            var mockReports = new List<ReportRecord>
            {
                new ReportRecord
                {
                    Id = Guid.NewGuid(),
                    Name = "Name 1",
                    ReportId = 1,
                },
                new ReportRecord
                {
                    Id = Guid.NewGuid(),
                    Name = "Name 2",
                    ReportId = 2,
                },
            };
            this.mockReportService.Setup(s => s.GetReportRecordsAsync()).Returns(mockReports.ToAsyncEnumerable());

            // Act
            var result = reportController.Get();

            // Assert
            Assert.That(result, Is.TypeOf<ActionResult<IEnumerable<ReportResponse>>>());
            var objectResult = result.Result as ObjectResult;
            var reportsResult = objectResult?.Value as IEnumerable<ReportResponse>;
            Assert.That(reportsResult?.First().Id, Is.EqualTo(mockReports[0].Id));
            Assert.That(reportsResult?.First().Name, Is.EqualTo(mockReports[0].Name));

            Assert.That(reportsResult?.Skip(1).First().Id, Is.EqualTo(mockReports[1].Id));
            Assert.That(reportsResult?.Skip(1).First().Name, Is.EqualTo(mockReports[1].Name));
        }

        [Test]
        public void Get_NoReports_ReturnsNoContent()
        {
            // Arrange
            var reportController = this.CreateReportController();
            this.mockReportService.Setup(s => s.GetReportRecordsAsync()).Returns(AsyncEnumerable.Empty<ReportRecord>());

            // Act
            var result = reportController.Get();

            // Assert
            Assert.That(result, Is.TypeOf<ActionResult<IEnumerable<ReportResponse>>>());
            Assert.That(result.Result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task GetById_ReportExists_ReportReturned()
        {
            // Arrange
            var reportController = this.CreateReportController();
            var mockReports = new List<ReportRecord>
            {
                new ReportRecord
                {
                    Id = Guid.NewGuid(),
                    Name = "Name 1",
                    ReportId = 1,
                },
                new ReportRecord
                {
                    Id = Guid.NewGuid(),
                    Name = "Name 2",
                    ReportId = 2,
                },
            };
            this.mockReportService.Setup(s => s.GetReportRecordByIdAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(mockReports[0]);

            // Act
            var result = await reportController.Get(mockReports[0].Id);

            // Assert
            Assert.That(result, Is.TypeOf<ActionResult<ReportResponse>>());
            var objectResult = result.Result as ObjectResult;
            var reportsResult = objectResult?.Value as ReportResponse;
            Assert.That(reportsResult?.Id, Is.EqualTo(mockReports[0].Id));
            Assert.That(reportsResult?.Name, Is.EqualTo(mockReports[0].Name));
        }

        [Test]
        public async Task GetById_ReportDoesNotExist_NoReportReturned()
        {
            // Arrange
            var reportController = this.CreateReportController();
            this.mockReportService.Setup(s => s.GetReportRecordByIdAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(() => null);

            // Act
            var result = await reportController.Get(Guid.NewGuid());

            // Assert
            Assert.That(result, Is.TypeOf<ActionResult<ReportResponse>>());
            Assert.That(result.Result, Is.TypeOf<NoContentResult>());
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
