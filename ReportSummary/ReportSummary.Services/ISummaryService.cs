namespace ReportSummary.Services
{
    public interface ISummaryService
    {
        Task<string?> GetReportSummaryAsync(Guid reportId);
    }
}