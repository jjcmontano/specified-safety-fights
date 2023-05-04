using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;
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

        public SummaryService(OpenAIClient openAIClient, IReportService reportService, IOptions<OpenAiConfiguration> openAiConfiguration, IOptions<CosmosConfiguration> cosmosConfiguration)
        {
            _openAIClient = openAIClient;
            _reportService = reportService;
            _openAiConfiguration = openAiConfiguration.Value;
            _cosmosConfiguration = cosmosConfiguration.Value;
        }

        public async Task<string?> GetReportSummaryAsync(Guid reportId)
        {
            var report = await _reportService.GetReportRecordByIdAsync(reportId, _cosmosConfiguration.DefaultPartitionKey);

            if (report != null)
            {
                var prompt = $"Summarise the following report down to exactly 5 bullet points. Each bullet point must have at least 20 words. Each bullet point must have a maximum of 40 words. Focus on the main trends of the {report.ReportSectorTitle} industry:\n" +
                    "\n" +
                    $"{report.CurrentPerformanceAnalysis?.ReplaceLineEndings(" ")} {report.OutlookAnalysis?.ReplaceLineEndings(" ")} {report.MajorMarketsAnalysis?.ReplaceLineEndings(" ")} {report.ProductsAndServicesAnalysis?.ReplaceLineEndings(" ")} {report.CostStructureBenchmarksAnalysis?.ReplaceLineEndings(" ")}";

                var completionOptions = new CompletionsOptions
                {
                    Prompts = { prompt },
                    MaxTokens = 200,
                    Temperature = 0f,
                    FrequencyPenalty = 0.0f,
                    PresencePenalty = 0.0f,
                    NucleusSamplingFactor = 1, // Top P
                };

                Completions response = await _openAIClient.GetCompletionsAsync(_openAiConfiguration.DeploymentModel, completionOptions);
                var textResult = response?.Choices?.FirstOrDefault()?.Text;

                return textResult;
            }

            return null;
        }
    }
}
