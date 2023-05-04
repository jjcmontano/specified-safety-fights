using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ReportSummary.Api.Model;
using ReportSummary.Configuration;
using ReportSummary.Model;
using ReportSummary.Services;
using static Azure.Core.HttpHeader;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReportSummary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly CosmosConfiguration _cosmosConfiguration;

        public ReportController(IReportService reportService, IOptions<CosmosConfiguration> cosmosConfiguration)
        {
            _reportService = reportService;
            _cosmosConfiguration = cosmosConfiguration.Value;
        }


        // GET: api/<ReportController>
        [HttpGet]
        public ActionResult<IEnumerable<ReportResponse>> Get()
        {
            var reports = _reportService.GetReportRecordsAsync().ToBlockingEnumerable();

            if (reports != null && reports.Any())
            {
                var reportResponses = reports.Select(report => new ReportResponse
                {
                    Id = report.Id,
                    Name = report.Name,
                    ReportId = report.ReportId,
                    ReportYear = report.ReportYear,
                    ReportCode = report.ReportCode,
                    ReportSectorTitle = report.ReportSectorTitle,
                    ReportTitle = report.ReportTitle,
                });
                return Ok(reportResponses);
            }

            return NoContent();
        }

        // GET api/<ReportController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportResponse>> Get([FromRoute] Guid id)
        {
            var report = await _reportService.GetReportRecordByIdAsync(id, _cosmosConfiguration.DefaultPartitionKey);

            if (report != null)
            {
                var reportResponse = new ReportResponse
                {
                    Id = report.Id,
                    Name = report.Name,
                    ReportId = report.ReportId,
                    ReportYear = report.ReportYear,
                    ReportCode = report.ReportCode,
                    ReportSectorTitle = report.ReportSectorTitle,
                    ReportTitle = report.ReportTitle,
                };
                return Ok(reportResponse);
            }

            return NoContent();
        }

        // POST api/<ReportController>
        [HttpPost]
        public async Task<ActionResult<ReportResponse>> Post(IFormFile reportFile)
        {
            using var reportStream = reportFile.OpenReadStream();
            var nowUtc = DateTimeOffset.UtcNow;
            var report = await _reportService.CreateReportAsync(reportStream, nowUtc, _cosmosConfiguration.DefaultPartitionKey);

            if (report != null)
            {
                var reportResponse = new ReportResponse
                {
                    Id = report.Id,
                    Name = report.Name,
                    ReportId = report.ReportId,
                    ReportYear = report.ReportYear,
                    ReportCode = report.ReportCode,
                    ReportSectorTitle = report.ReportSectorTitle,
                    ReportTitle = report.ReportTitle,
                };
                return Ok(reportResponse);
            }

            return NoContent();
        }

        //// PUT api/<ReportController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ReportController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _reportService.DeleteReportAsync(id, _cosmosConfiguration.DefaultPartitionKey);

            return NoContent();
        }
    }
}
