using Microsoft.AspNetCore.Mvc;

namespace LoadGeneratorService.Controllers
{
    [Route("probe")]
    public class HealthController : Controller
    {
        // GET api/load/5
        [HttpGet("health")]
        public bool Health()
        {
            return true;
        }

        // GET api/load/5
        [HttpGet("readiness")]
        public bool Readiness()
        {
            return true;
        }
    }
}