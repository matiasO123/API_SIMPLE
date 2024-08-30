using Microsoft.AspNetCore.Mvc;

namespace API_Rest_Simple.Controllers
{
    [Route("api/bad")]
    [ApiController]
    public class BadController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return BadRequest();
        }
    }
}
