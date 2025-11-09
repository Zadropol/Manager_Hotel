using AutoMapper;
using FluentValidation;
using Hotel_Manager.Core.CustomEntities;
using Hotel_Manager.Core.Entities;
using Hotel_Manager.Core.QueryFilters;
using Hotel_Manager.Infrastructure.DTO;
using Hotel_Manager.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Manager.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _service;
        private readonly IMapper _mapper;
        private readonly IValidator<BookingDTO> _validator;

        public BookingController(BookingService service, IMapper mapper, IValidator<BookingDTO> validator)
        {
            _service = service;
            _mapper = mapper;
            _validator = validator;
        }

        /// <summary>
        /// Crea una nueva reserva para un huésped y habitación.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingDTO dto)
        {
            var validation = await _validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(new { Errors = validation.Errors.Select(e => e.ErrorMessage) });

            var booking = _mapper.Map<BookingEntity>(dto);
            var created = await _service.CreateBookingAsync(booking);
            var result = _mapper.Map<BookingDTO>(created);

            return CreatedAtAction(nameof(GetBookingById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Obtiene una reserva por su ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _service.GetBookingByIdAsync(id);
            return booking == null ? NotFound() : Ok(booking);
        }

        /// <summary>
        /// Obtiene habitaciones disponibles entre dos fechas.
        /// </summary>
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableRooms(DateTime startDate, DateTime endDate)
        {
            var rooms = await _service.GetAvailableRoomsAsync(startDate, endDate);
            return Ok(rooms);
        }
        public async Task<IActionResult> GetAll([FromQuery] BookingQueryFilter filters)
        {
            var bookings = await _service.GetBookingsAsync(filters);
            var dtoList = _mapper.Map<IEnumerable<BookingDTO>>(bookings);

            var metadata = new Paged
            {
                TotalCount = bookings.TotalCount,
                PageSize = bookings.PageSize,
                CurrentPage = bookings.CurrentPage,
                TotalPages = bookings.TotalPages,
                HasNextPage = bookings.HasNext,
                HasPreviousPage = bookings.HasPrevious
            };

            var response = new ResponseData<IEnumerable<BookingDTO>>(dtoList)
            {
                Meta = metadata,
                Message = "Reservas obtenidas correctamente"
            };

            return Ok(response);
        }

    }
}
