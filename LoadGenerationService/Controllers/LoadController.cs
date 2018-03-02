using System;
using System.Threading.Tasks;
using LoadGeneratorService.LoadGenerator;
using Microsoft.AspNetCore.Mvc;

namespace LoadGeneratorService.Controllers
{
    [Route("api/load")]
    public class LoadController : Controller
    {
        private readonly ILoad _load;
        public LoadController(ILoad load)
        {
            _load = load;
        }

        // GET api/load/5
        [HttpGet("{loadValue}")]
        public async Task<string> Get(int loadValue)
        {
            var validationRequest = HttpContext.Request.Query["validate"].ToString();
            var primes = await _load.ExecuteLoad(loadValue, !IsNullOrEmptyString(validationRequest));
            return string.Join(", ", primes);
        }

        private bool IsNullOrEmptyString(string val)
        {
            if (val == null)
            {
                return true;
            }

            if (val.Equals(string.Empty))
            {
                return true;
            }

            return false;
        }
    }
}
