using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.RecordIO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReportSummary.Configuration;
using ReportSummary.Model;
using System.Text.Json;

namespace ReportSummary.Services
{
    public class ReportService : IReportService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly CosmosConfiguration _cosmosConfiguration;
        private readonly ILogger<ReportService> _logger;

        public ReportService(CosmosClient cosmosClient, IOptions<CosmosConfiguration> cosmosConfiguration, ILogger<ReportService> logger)
        {
            _cosmosClient = cosmosClient;
            _cosmosConfiguration = cosmosConfiguration.Value;
            _logger = logger;
        }

        private Container Container { get => _cosmosClient.GetDatabase(_cosmosConfiguration.Database).GetContainer(_cosmosConfiguration.Container); }

        public async IAsyncEnumerable<ReportRecord> GetReportRecordsAsync()
        {
            var reportsQueryable = Container.GetItemLinqQueryable<ReportRecord>();

            using FeedIterator<ReportRecord> reportIterator = reportsQueryable
                .OrderByDescending(r => r.Name)
                .ToFeedIterator();

            while (reportIterator.HasMoreResults)
            {
                var response = await reportIterator.ReadNextAsync();
                foreach (var report in response)
                {
                    yield return report;
                }
            }
        }

        public async Task<ReportRecord?> GetReportRecordByIdAsync(Guid reportId, string? partitionKey = null)
        {
            var reportResult = await Container.ReadItemAsync<ReportRecord>(reportId.ToString(), new PartitionKey(partitionKey));

            if (reportResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return reportResult.Resource;
            }

            return null;
        }

        private async Task<ReportRecord?> ConvertFileToReportRecord(Stream inputFileStream, DateTimeOffset nowUtc)
        {
            try
            {
                var report = await JsonSerializer.DeserializeAsync<Report>(inputFileStream);

                if (report == null)
                {
                    return null;
                }
                
                return new ReportRecord
                {
                    Name = report.ReportTitle?.Title,
                    PartitionKey = _cosmosConfiguration.DefaultPartitionKey,
                    CreatedDateTimeOffset = nowUtc,
                    ReportId = report.ReportID?.ID,
                    ReportYear = report.ReportYear?.Year,
                    ReportCode = report.ReportCode?.Code,
                    ReportSectorTitle = report.ReportSectorTitle?.Title,
                    ReportTitle = report.ReportTitle?.Title,
                    CurrentPerformanceAnalysis = report.CurrentPerformanceAnalysis?.AnalysisPlainText,
                    OutlookAnalysis = report.OutlookAnalysis?.AnalysisPlainText,
                    MajorMarketsAnalysis = report.MajorMarketsAnalysis?.AnalysisPlainText,
                    ProductsAndServicesAnalysis = report.ProductsAndServicesAnalysis?.AnalysisPlainText,
                    CostStructureBenchmarksAnalysis = report.CostStructureBenchmarksAnalysis?.AnalysisPlainText,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to convert report file to JSON");
                return null;
            }
        }

        public async Task<ReportRecord?> CreateReportAsync(Stream inputFileStream, DateTimeOffset nowUtc, string? partitionKey = null)
        {
            var parsedReport = await ConvertFileToReportRecord(inputFileStream, nowUtc);

            if (parsedReport == null)
            {
                return null;
            }

            var reportResult = await Container.CreateItemAsync(parsedReport, new PartitionKey(partitionKey));

            if (reportResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var reportRecord = reportResult.Resource;
                reportRecord.Id = Guid.NewGuid();
                return reportRecord;
            }

            return null;
        }
    }
}