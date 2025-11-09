using Hotel_Manager.Core.CustomEntities;
using Hotel_Manager.Core.Entities;
using Hotel_Manager.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Core.Interfaces
{
    public interface IBookingRepository : IBaseRepository<BookingEntity>
    {
        Task<IEnumerable<RoomEntity>> GetAvailableRooms(DateTime startDate, DateTime endDate);
        Task<PagedList<BookingEntity>> GetBookingsAsync(BookingQueryFilter filters);
    }
}
