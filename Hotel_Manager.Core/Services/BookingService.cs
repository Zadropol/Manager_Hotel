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

namespace Hotel_Manager.Core.Services
{
    public class BookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PagedList<BookingEntity>> GetBookingsAsync(BookingQueryFilter filters)
        {
            var bookings = await _unitOfWork.Bookings.GetBookingsAsync(filters);
            if (!bookings.Any())
                throw new BusinessException("No se encontraron reservas con los filtros especificados.");

            return bookings;
        }

        public async Task<BookingEntity> CreateBookingAsync(BookingEntity booking)
        {
            if (booking.CheckInDate < DateTime.Today)
                throw new BusinessException("La fecha de Check-In no puede ser anterior a hoy.");

            if (booking.CheckOutDate <= booking.CheckInDate)
                throw new BusinessException("La fecha de Check-Out debe ser posterior al Check-In.");

            var guest = await _unitOfWork.Guests.GetByIdAsync(booking.GuestId);
            if (guest == null)
                throw new BusinessException("El huésped no existe en el sistema.");

            var room = await _unitOfWork.Rooms.GetByIdAsync(booking.RoomId);
            if (room == null)
                throw new BusinessException("La habitación seleccionada no existe.");

            var availableRooms = await _unitOfWork.Bookings.GetAvailableRooms(
                booking.CheckInDate, booking.CheckOutDate);

            if (!availableRooms.Any(r => r.Id == booking.RoomId))
                throw new BusinessException("La habitación no está disponible en esas fechas.");

            booking.StatusId = 1; // Pendiente
            booking.TotalPrice = room.PricePerNight *
                (decimal)(booking.CheckOutDate - booking.CheckInDate).TotalDays;

            await _unitOfWork.Bookings.AddAsync(booking);
            return booking;
        }

        public async Task<BookingEntity?> GetBookingByIdAsync(int id)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (booking == null)
                throw new BusinessException("La reserva solicitada no existe.");

            return booking;
        }

        public async Task<IEnumerable<RoomEntity>> GetAvailableRoomsAsync(DateTime start, DateTime end)
        {
            if (start >= end)
                throw new BusinessException("La fecha de inicio debe ser menor a la de fin.");

            var rooms = await _unitOfWork.Bookings.GetAvailableRooms(start, end);
            if (!rooms.Any())
                throw new BusinessException("No se encontraron habitaciones disponibles en ese rango.");

            return rooms;
        }
    }
}
