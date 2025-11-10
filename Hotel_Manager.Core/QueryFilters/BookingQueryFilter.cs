using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Core.QueryFilters
{
    public class BookingQueryFilter
    {
        public int? GuestId { get; set; }
        public int? RoomId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

   
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
