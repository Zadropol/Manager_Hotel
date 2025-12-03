using Microsoft.AspNetCore.Mvc;

namespace Hotel_Manager.API.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Version = "v2",
                Message = "Listado avanzado de habitaciones (v2)",
                Changes = "Incluye precios y disponibilidad"
            });
        }
    }
}
