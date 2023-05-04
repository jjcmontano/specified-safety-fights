using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.RecordIO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using ReportSummary.Configuration;
using ReportSummary.Model;
using ReportSummary.Services;
using System;
using System.Threading.Tasks;

namespace ReportSummary.Services.Tests
{
    [TestFixture]
    public class ReportServiceTests
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        // These fields will be initialised during SetUp() rather than in the constructor
        private MockRepository mockRepository;

        private Mock<CosmosClient> mockCosmosClient;
        private IOptions<CosmosConfiguration> mockOptions;
        private Mock<ILogger<ReportService>> mockLogger;
        private Mock<Database> mockDatabase;
        private Mock<Container> mockContainer;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Default);

            this.mockDatabase = new Mock<Database>();
            this.mockContainer = new Mock<Container>();

            mockDatabase.Setup(_ => _.GetContainer(It.IsAny<string>())).Returns(this.mockContainer.Object);

            this.mockCosmosClient = this.mockRepository.Create<CosmosClient>();
            this.mockCosmosClient.Setup(_ => _.GetDatabase(It.IsAny<string>())).Returns(this.mockDatabase.Object);

            this.mockOptions = Options.Create(new CosmosConfiguration
            {
                Container = "Container",
                Database = "Database",
                Endpoint = "Endpoint",
                DefaultPartitionKey = "DefaultPartitionKey",
                PrimaryKey = "PrimaryKey",
            });

            this.mockLogger = this.mockRepository.Create<ILogger<ReportService>>();
        }

        private ReportService CreateService()
        {
            return new ReportService(
                this.mockCosmosClient.Object,
                this.mockOptions,
                this.mockLogger.Object);
        }

        //[Test]
        //public async Task GetReportRecordsAsync_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var service = this.CreateService();

        //    // Act
        //    var result = await service.GetReportRecordsAsync();

        //    // Assert
        //    Assert.Fail();
        //    this.mockRepository.VerifyAll();
        //}

        //[Test]
        //public async Task GetReportRecordByIdAsync_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var service = this.CreateService();
        //    Guid reportId = default(global::System.Guid);
        //    string? partitionKey = null;

        //    // Act
        //    var result = await service.GetReportRecordByIdAsync(
        //        reportId,
        //        partitionKey);

        //    // Assert
        //    Assert.Fail();
        //    this.mockRepository.VerifyAll();
        //}

        [Test]
        public async Task CreateReportAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var service = this.CreateService();
            using var inputFileStream = File.OpenRead(@"Assets\SampleReport.json");
            var nowUtc = DateTimeOffset.UtcNow;
            var expectedReport = new ReportRecord
            {
                Name = "Dentists in the US",
                PartitionKey = "DefaultPartitionKey",
                CreatedDateTimeOffset = nowUtc,
                ReportId = 1557,
                ReportYear = 2023,
                ReportCode = "62121",
                ReportSectorTitle = "Healthcare and Social Assistance",
                ReportTitle = "Dentists in the US",
                CurrentPerformanceAnalysis = "Foo",
                OutlookAnalysis = "Foo",
                MajorMarketsAnalysis = "Foo",
                ProductsAndServicesAnalysis = "Foo",
                CostStructureBenchmarksAnalysis = "Foo",
            };
            var mockItemResponse = new Mock<ItemResponse<ReportRecord>>();
            mockItemResponse.SetupAllProperties();
            mockItemResponse.SetupGet(_ => _.Resource).Returns(expectedReport);
            mockItemResponse.SetupGet(_ => _.StatusCode).Returns(System.Net.HttpStatusCode.OK);
            this.mockContainer.Setup(_ => _.CreateItemAsync(
                It.IsAny<ReportRecord>(),
                It.IsAny<PartitionKey>(),
                It.IsAny<ItemRequestOptions>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(mockItemResponse.Object);

            // Act
            var result = await service.CreateReportAsync(
                inputFileStream,
                nowUtc);

            // Assert

            Assert.That(result?.Name, Is.EqualTo(expectedReport.Name));
            Assert.That(result?.PartitionKey, Is.EqualTo(expectedReport.PartitionKey));
            Assert.That(result?.CreatedDateTimeOffset, Is.EqualTo(expectedReport.CreatedDateTimeOffset));
            Assert.That(result?.ReportId, Is.EqualTo(expectedReport.ReportId));
            Assert.That(result?.ReportYear, Is.EqualTo(expectedReport.ReportYear));
            Assert.That(result?.ReportCode, Is.EqualTo(expectedReport.ReportCode));
            Assert.That(result?.ReportSectorTitle, Is.EqualTo(expectedReport.ReportSectorTitle));
            Assert.That(result?.ReportTitle, Is.EqualTo(expectedReport.ReportTitle));
            Assert.That(result?.CurrentPerformanceAnalysis, Is.EqualTo(expectedReport.CurrentPerformanceAnalysis));
            Assert.That(result?.OutlookAnalysis, Is.EqualTo(expectedReport.OutlookAnalysis));
            Assert.That(result?.MajorMarketsAnalysis, Is.EqualTo(expectedReport.MajorMarketsAnalysis));
            Assert.That(result?.ProductsAndServicesAnalysis, Is.EqualTo(expectedReport.ProductsAndServicesAnalysis));
            Assert.That(result?.CostStructureBenchmarksAnalysis, Is.EqualTo(expectedReport.CostStructureBenchmarksAnalysis));
        }
    }
}
