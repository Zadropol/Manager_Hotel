using AutoMapper;
using FluentValidation;
using Hotel_Manager.API.Responses;
using Hotel_Manager.Core.CustomEntities;
using Hotel_Manager.Core.Entities;
using Hotel_Manager.Core.QueryFilters;
using Hotel_Manager.Infrastructure.DTO;
using Hotel_Manager.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Manager.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _service;
        private readonly IMapper _mapper;


        public BookingController(BookingService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todas las reservas con filtros y paginación.
        /// </summary>
        [HttpGet]
        public IActionResult GetAll([FromQuery] BookingQueryFilter filters)
        {
            var bookings = _service.GetBookings(filters);
            return Ok(bookings);
        }


        /// <summary>
        /// Crea una nueva reserva para un huésped.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingDTO dto)
        {
            var booking = _mapper.Map<BookingEntity>(dto);
            var created = await _service.CreateBookingAsync(booking);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Obtiene una reserva por su ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var booking = await _service.GetBookingByIdAsync(id);
            return Ok(booking);
        }

        /// <summary>
        /// Obtiene habitaciones disponibles en un rango de fechas.
        /// </summary>
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableRooms(DateTime startDate, DateTime endDate)
        {
            var rooms = await _service.GetAvailableRoomsAsync(startDate, endDate);
            return Ok(rooms);
        }

    }
}
