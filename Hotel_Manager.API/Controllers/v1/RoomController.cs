using Microsoft.AspNetCore.Mvc;

namespace Hotel_Manager.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Version = "v1",
                Message = "Listado básico de habitaciones (v1)"
            });
        }
    }
}
