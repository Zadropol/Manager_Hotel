using Hotel_Manager.Core.CustomEntities;
using Hotel_Manager.Core.Entities;
using Hotel_Manager.Core.Interfaces;
using Hotel_Manager.Core.QueryFilters;
using Hotel_Manager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Infrastructure.Repositories
{
    public class BookingRepository : BaseRepository<BookingEntity>, IBookingRepository
    {
        public BookingRepository(Hotel_ManagerDbContext context) : base(context) { }

        public async Task<IEnumerable<RoomEntity>> GetAvailableRooms(DateTime startDate, DateTime endDate)
        {
            var reservedRooms = await _context.Bookings
                .Where(b =>
                    b.StatusId != 5 &&
                    ((startDate >= b.CheckInDate && startDate < b.CheckOutDate) ||
                     (endDate > b.CheckInDate && endDate <= b.CheckOutDate)))
                .Select(b => b.RoomId)
                .ToListAsync();

            return await _context.Rooms
                .Where(r => !reservedRooms.Contains(r.Id))
                .ToListAsync();
        }

        public async Task<PagedList<BookingEntity>> GetBookingsAsync(BookingQueryFilter filters)
        {
            var query = _context.Bookings
                .Include(b => b.Guest)
                .Include(b => b.Room)
                .AsQueryable();

            if (filters.GuestId.HasValue)
                query = query.Where(b => b.GuestId == filters.GuestId.Value);

            if (filters.RoomId.HasValue)
                query = query.Where(b => b.RoomId == filters.RoomId.Value);

            if (filters.StartDate.HasValue)
                query = query.Where(b => b.CheckInDate >= filters.StartDate.Value);

            if (filters.EndDate.HasValue)
                query = query.Where(b => b.CheckOutDate <= filters.EndDate.Value);

            return await PagedList<BookingEntity>.CreateAsync(query, filters.PageNumber, filters.PageSize);
        }

    }
}
