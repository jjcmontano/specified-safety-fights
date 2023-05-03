using ReportSummary.Model;

namespace ReportSummary.Services
{
    public interface IReportService
    {
        Task<ReportRecord?> GetReportById(Guid reportId, string? partitionKey = null);
        IAsyncEnumerable<ReportRecord> GetReportRecordsAsync();
    }
}