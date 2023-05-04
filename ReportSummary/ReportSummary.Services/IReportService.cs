using ReportSummary.Model;

namespace ReportSummary.Services
{
    public interface IReportService
    {
        Task<ReportRecord?> GetReportRecordById(Guid reportId, string? partitionKey = null);
        IAsyncEnumerable<ReportRecord> GetReportRecordsAsync();
    }
}