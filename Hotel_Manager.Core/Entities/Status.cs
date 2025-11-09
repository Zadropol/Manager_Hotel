using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Core.Entities
{
    public class Status : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<BookingEntity>? Bookings { get; set; }
    }
}
