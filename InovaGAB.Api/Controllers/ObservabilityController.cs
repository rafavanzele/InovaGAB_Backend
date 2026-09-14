using InovaGAB.Api.Observability;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InovaGAB.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Lideranca")]
    public class ObservabilityController : ControllerBase
    {
        private readonly ApiMetrics _metrics;

        public ObservabilityController(ApiMetrics metrics)
        {
            _metrics = metrics;
        }

        [HttpGet("metrics")]
        public IActionResult GetMetrics()
        {
            return Ok(_metrics.GetSnapshot());
        }
    }
}