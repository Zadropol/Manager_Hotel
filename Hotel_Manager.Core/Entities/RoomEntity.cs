using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Core.Entities
{
    public class RoomEntity :BaseEntity
    {
        public string RoomNumber { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public ICollection<BookingEntity>? Bookings { get; set; }
    }
}
