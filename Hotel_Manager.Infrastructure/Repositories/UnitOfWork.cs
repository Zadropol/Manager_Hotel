using Hotel_Manager.Core.Entities;
using Hotel_Manager.Core.Interfaces;
using Hotel_Manager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Hotel_ManagerDbContext _context;

        private readonly IDbConnectionFactory _Factory;
        private IDbContextTransaction? _transaction;
        public IBookingRepository Bookings { get; }
        public IBaseRepository<GuestEntity> Guests { get; }
        public IBaseRepository<RoomEntity> Rooms { get; }
        public IBaseRepository<Status> Statuses { get; }

        public UnitOfWork(Hotel_ManagerDbContext context, IDbConnectionFactory factory)
        {
            _context = context;
            _Factory = factory;

            Bookings = new BookingRepository(_context);
            Guests = new BaseRepository<GuestEntity>(_context);
            Rooms = new BaseRepository<RoomEntity>(_context);
            Statuses = new BaseRepository<Status>(_context);
        }
        public async Task BeginTransactionAsync()
        {
            _transaction ??= await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }

    }
}
