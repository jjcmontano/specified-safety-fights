using Azure.AI.OpenAI;
using ReportSummary.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace ReportSummary.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly OpenAIClient _openAIClient;
        private readonly IReportService _reportService;
        private readonly OpenAiConfiguration _openAiConfiguration;
        private readonly CosmosConfiguration _cosmosConfiguration;

        public SummaryService(OpenAIClient openAIClient, IReportService reportService, OpenAiConfiguration openAiConfiguration, CosmosConfiguration cosmosConfiguration)
        {
            _openAIClient = openAIClient;
            _reportService = reportService;
            _openAiConfiguration = openAiConfiguration;
            _cosmosConfiguration = cosmosConfiguration;
        }

        public async Task<string?> GetReportSummaryAsync(Guid reportId)
        {
            var report = await _reportService.GetReportRecordByIdAsync(reportId, _cosmosConfiguration.DefaultPartitionKey);

            if (report != null)
            {
                var prompt = "Summarise the following report down to exactly 5 bullet points. Each bullet point must composed of at least 20 words up to a maximum of 40 words each. Focus on the main trends of the industry mentioned in the report:\n" +
                    "\n" +
                    $"{report.CurrentPerformanceAnalysis} {report.OutlookAnalysis} {report.MajorMarketsAnalysis} {report.ProductsAndServicesAnalysis} {report.CostStructureBenchmarksAnalysis}";

                var completionOptions = new CompletionsOptions
                {
                    Prompts = { prompt },
                    MaxTokens = 64,
                    Temperature = 0f,
                    FrequencyPenalty = 0.0f,
                    PresencePenalty = 0.0f,
                    NucleusSamplingFactor = 1 // Top P
                };

                Completions response = await _openAIClient.GetCompletionsAsync(_openAiConfiguration.DeploymentModel, completionOptions);
                return response?.Choices?.FirstOrDefault()?.Text;
            }

            return null;
        }
    }
}
