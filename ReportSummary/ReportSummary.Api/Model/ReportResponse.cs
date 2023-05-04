namespace ReportSummary.Api.Model
{
    public class ReportResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int? ReportId { get; set; }
        public int? ReportYear { get; set; }
        public string? ReportCode { get; set; }
        public string? ReportSectorTitle { get; set; }
        public string? ReportTitle { get; set; }
    }
}
