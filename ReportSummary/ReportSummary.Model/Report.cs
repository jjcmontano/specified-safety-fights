using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportSummary.Model
{
    public class Report
    {
        public ReportYear? ReportYear { get; set; }
        public ReportID? ReportID { get; set; }
        public ReportCode? ReportCode { get; set; }
        public ReportCollectionType? ReportCollectionType { get; set; }
        public ReportCollectionCode? ReportCollectionCode { get; set; }
        public ReportSectorCode? ReportSectorCode { get; set; }
        public ReportSectorTitle? ReportSectorTitle { get; set; }
        public ReportSectorComparisonCode? ReportSectorComparisonCode { get; set; }
        public ReportSectorComparisonTitle? ReportSectorComparisonTitle { get; set; }
        public ReportTitle? ReportTitle { get; set; }
        public PublishedDate? PublishedDate { get; set; }
        public PublishedDateFormatted? PublishedDateFormatted { get; set; }
        public Author? Author { get; set; }
        public object? FinancialRatios { get; set; }
        public Analysis? CurrentPerformanceAnalysis { get; set; }
        public Analysis? OutlookAnalysis { get; set; }
        public Analysis? MajorMarketsAnalysis { get; set; }
        public Analysis? ProductsAndServicesAnalysis { get; set; }
        public Analysis? CostStructureBenchmarksAnalysis { get; set; }
    }

    public class Author
    {
        public string? AuthorName { get; set; }
    }

    public class Analysis
    {
        public string? AnalysisPlainText { get; set; }
    }

    public class PublishedDate
    {
        public DateTime Date { get; set; }
    }

    public class PublishedDateFormatted
    {
        public string? Date { get; set; }
    }

    public class ReportCode
    {
        public string? Code { get; set; }
    }

    public class ReportCollectionCode
    {
        public string? CollectionCode { get; set; }
    }

    public class ReportCollectionType
    {
        public string? CollectionType { get; set; }
    }

    public class ReportID
    {
        public int ID { get; set; }
    }

    public class ReportSectorCode
    {
        public string? Code { get; set; }
    }

    public class ReportSectorComparisonCode
    {
        public string? Code { get; set; }
    }

    public class ReportSectorComparisonTitle
    {
        public string? Title { get; set; }
    }

    public class ReportSectorTitle
    {
        public string? Title { get; set; }
    }

    public class ReportTitle
    {
        public string? Title { get; set; }
    }

    public class ReportYear
    {
        public int? Year { get; set; }
    }
}
