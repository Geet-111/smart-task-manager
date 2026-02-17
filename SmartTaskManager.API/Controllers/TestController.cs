using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartTaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult Public()
        {
            return Ok("This is public data.");
        }

        [HttpGet("private")]
        [Authorize]
        public IActionResult Private()
        {
            return Ok("This is private data 🔒");
        }
    }
}
