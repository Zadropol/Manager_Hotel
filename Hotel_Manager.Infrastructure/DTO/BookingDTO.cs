using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Infrastructure.DTO
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public int StatusId { get; set; }
        public string CheckInDate { get; set; } = null!;
        public string CheckOutDate { get; set; } = null!;
        public decimal TotalPrice { get; set; }

    }
}
