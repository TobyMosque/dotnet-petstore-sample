using Microsoft.AspNetCore.Mvc;

namespace PetStore.Api.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok();
    }
}
