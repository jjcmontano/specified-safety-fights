using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportSummary.Services;

namespace ReportSummary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private readonly ISummaryService _summaryService;

        public SummaryController(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        [HttpGet("{reportRecordId}")]
        public async Task<ActionResult<string>> Get([FromRoute] Guid reportRecordId)
        {
            var summaryResult = await _summaryService.GetReportSummaryAsync(reportRecordId);

            if (summaryResult != null)
            {
                return Ok(summaryResult);
            }

            return NoContent();
        }
    }
}
