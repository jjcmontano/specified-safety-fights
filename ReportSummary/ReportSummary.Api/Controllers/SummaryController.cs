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

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get([FromRoute] Guid id)
        {
            var summaryResult = await _summaryService.GetReportSummaryAsync(id);

            if (summaryResult != null)
            {
                return Ok(summaryResult);
            }

            return NoContent();
        }
    }
}
