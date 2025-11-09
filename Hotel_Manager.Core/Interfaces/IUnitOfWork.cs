using Hotel_Manager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Core.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IBookingRepository Bookings { get; }
        IBaseRepository<GuestEntity> Guests { get; }
        IBaseRepository<RoomEntity> Rooms { get; }
        IBaseRepository<Status> Statuses { get; }
        Task SaveChangesAsync();
    }
}
