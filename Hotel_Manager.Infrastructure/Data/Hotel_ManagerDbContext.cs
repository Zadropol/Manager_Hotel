using Hotel_Manager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Infrastructure.Data
{
    public class Hotel_ManagerDbContext : DbContext
    {
        public Hotel_ManagerDbContext(DbContextOptions<Hotel_ManagerDbContext> options) : base(options) { }

        public DbSet<GuestEntity> Guests => Set<GuestEntity>();
        public DbSet<RoomEntity> Rooms => Set<RoomEntity>();
        public DbSet<BookingEntity> Bookings => Set<BookingEntity>();
        public DbSet<Status> Statuses => Set<Status>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GuestEntity>().ToTable("Guests");
            modelBuilder.Entity<RoomEntity>().ToTable("Rooms");
            modelBuilder.Entity<BookingEntity>().ToTable("Bookings");
            modelBuilder.Entity<Status>().ToTable("Statuses");

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "Pendiente" },
                new Status { Id = 2, Name = "Confirmada" },
                new Status { Id = 3, Name = "Check-In" },
                new Status { Id = 4, Name = "Check-Out" },
                new Status { Id = 5, Name = "Cancelada" }
                );
        }
    }
}
