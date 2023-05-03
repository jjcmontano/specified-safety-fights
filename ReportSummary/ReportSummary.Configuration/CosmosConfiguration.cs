namespace ReportSummary.Configuration
{
    public class CosmosConfiguration
    {
        public string? Endpoint { get; set; }
        public string? PrimaryKey { get; set; }
        public string? Database { get; set; }
        public string? Container { get; set; }
        public string? DefaultPartitionKey { get; set; }
    }
}