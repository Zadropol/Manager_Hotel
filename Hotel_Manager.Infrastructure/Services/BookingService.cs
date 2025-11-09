using Hotel_Manager.Core.CustomEntities;
using Hotel_Manager.Core.Entities;
using Hotel_Manager.Core.Exceptions;
using Hotel_Manager.Core.Interfaces;
using Hotel_Manager.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hotel_Manager.Infrastructure.Services
{
    public class BookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<BookingEntity>> CreateBookingAsync(BookingQueryFilter filters)
        {
            // Validaciones básicas
            //if (booking.CheckInDate < DateTime.Today)
            //    throw new Exception("La fecha de Check-In no puede ser anterior a hoy.");

            //if (booking.CheckOutDate <= booking.CheckInDate)
            //    throw new Exception("La fecha de Check-Out debe ser posterior al Check-In.");

            //var guest = await _unitOfWork.Guests.GetByIdAsync(booking.GuestId)
            //    ?? throw new Exception("El huésped no existe.");

            //var room = await _unitOfWork.Rooms.GetByIdAsync(booking.RoomId)
            //    ?? throw new Exception("La habitación no existe.");

            //var available = await _unitOfWork.Bookings.GetAvailableRooms(booking.CheckInDate, booking.CheckOutDate);
            //if (!available.Any(r => r.Id == booking.RoomId))
            //    throw new Exception("La habitación seleccionada no está disponible en esas fechas.");

            //booking.StatusId = 1; // Pendiente
            //booking.TotalPrice = room.PricePerNight *
            //    (decimal)(booking.CheckOutDate - booking.CheckInDate).TotalDays;

            //await _unitOfWork.Bookings.AddAsync(booking);
            //return booking;

            var bookings = await _unitOfWork.Bookings.GetBookingsAsync(filters);
            if (!bookings.Any())
                throw new BusinessException("No se encontraron reservas con los filtros especificados.");

            return bookings;
        }

        public async Task<IEnumerable<RoomEntity>> GetAvailableRoomsAsync(DateTime start, DateTime end) =>
            await _unitOfWork.Bookings.GetAvailableRooms(start, end);

        public async Task<BookingEntity?> GetBookingByIdAsync(int id) =>
            await _unitOfWork.Bookings.GetByIdAsync(id);

    }
}
