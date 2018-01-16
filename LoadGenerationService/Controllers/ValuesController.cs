using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LoadGeneratorService.Controllers
{
    [Route("api/load")]
    public class ValuesController : Controller
    {
        private readonly IInternalLoadGenerator loadGenerator;
        public ValuesController(IInternalLoadGenerator loadGenerator)
        {
            this.loadGenerator = loadGenerator;
        }

        // GET api/load/5
        [HttpGet("{load_value}")]
        public async Task<string> Get(int load_value)
        {
            var validationRequest = HttpContext.Request.Query["validate"].ToString();
            var primes = await loadGenerator.GenerateLoad(load_value, !IsNullOrEmptyString(validationRequest));
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
