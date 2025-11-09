using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Core.Entities
{
    public class BookingEntity : BaseEntity
    {
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public int StatusId { get; set; } = 1;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public GuestEntity? Guest { get; set; }
        public RoomEntity? Room { get; set; }
        public Status? Status { get; set; }

    }
}
