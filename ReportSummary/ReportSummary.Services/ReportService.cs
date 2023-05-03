using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.RecordIO;
using Microsoft.Extensions.Options;
using ReportSummary.Configuration;
using ReportSummary.Model;

namespace ReportSummary.Services
{
    public class ReportService : IReportService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly CosmosConfiguration _cosmosConfiguration;

        public ReportService(CosmosClient cosmosClient, IOptions<CosmosConfiguration> cosmosConfiguration)
        {
            _cosmosClient = cosmosClient;
            _cosmosConfiguration = cosmosConfiguration.Value;
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

        public async Task<ReportRecord?> GetReportById(Guid reportId, string? partitionKey = null)
        {
            var reportResult = await Container.ReadItemAsync<ReportRecord>(reportId.ToString(), new PartitionKey(partitionKey));

            if (reportResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return reportResult.Resource;
            }

            return null;
        }
    }
}