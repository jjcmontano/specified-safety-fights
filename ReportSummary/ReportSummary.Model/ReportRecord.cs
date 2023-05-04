namespace ReportSummary.Model
{
    public class ReportRecord
    {
        #region Utility Fields
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? PartitionKey { get; set; }
        public DateTimeOffset? CreatedDateTimeOffset { get; set; }
        #endregion Utility Fields

        #region Report Identification
        public int? ReportId { get; set; }
        public int? ReportYear { get; set; }
        public string? ReportCode { get; set; }
        public string? ReportSectorTitle { get; set; }
        public string? ReportTitle { get; set; }
        #endregion Report Identification

        #region Summary Fields
        public string? CurrentPerformanceAnalysis { get; set; }
        public string? OutlookAnalysis { get; set; }
        public string? MajorMarketsAnalysis { get; set; }
        public string? ProductsAndServicesAnalysis { get; set; }
        public string? CostStructureBenchmarksAnalysis { get; set; }
        #endregion Summary Fields
    }
}