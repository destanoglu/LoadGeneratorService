using Microsoft.AspNetCore.Mvc;

namespace ControllerExtension
{
    [Route("api/addition")]
    public class AdditionController : Controller
    {
        [HttpGet("add/{first}/{second}")]
        public string Index(int first, int second)
        {
            return (first +second).ToString();
        }
    }
}