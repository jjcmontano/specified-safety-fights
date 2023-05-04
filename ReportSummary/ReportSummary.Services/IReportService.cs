using ReportSummary.Model;

namespace ReportSummary.Services
{
    public interface IReportService
    {
        Task<ReportRecord?> CreateReportAsync(Stream inputFileStream, DateTimeOffset nowUtc, string? partitionKey = null);
        Task DeleteReportAsync(Guid reportId, string? partitionKey = null);
        Task<ReportRecord?> GetReportRecordByIdAsync(Guid reportId, string? partitionKey = null);
        IAsyncEnumerable<ReportRecord> GetReportRecordsAsync();
    }
}