using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Infrastructure.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
