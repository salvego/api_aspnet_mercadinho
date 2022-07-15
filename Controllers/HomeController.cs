using Microsoft.AspNetCore.Mvc;

namespace Mercadinho.Controllers
{
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}