using Microsoft.EntityFrameworkCore;
using HotelReservationSystem.Models;
using System;

namespace HotelReservationSystem.Data
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }  
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin" },
                new Role { RoleId = 2, RoleName = "Receptionist" },
                new Role { RoleId = 3, RoleName = "Guest" }
            );
            
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Name = "Petar Petrović", Email = "petar@example.com", Password = "1234", RoleId = 1 },
                new User { UserId = 2, Name = "Jelena Janković", Email = "jelena@example.com", Password = "1234", RoleId = 2 },
                new User { UserId = 3, Name = "Marko Marković", Email = "marko@example.com", Password = "1234", RoleId = 3 }
            );
            
            modelBuilder.Entity<Room>().HasData(
                new Room { RoomId = 1, RoomNumber = "101", Capacity = 2, PricePerNight = 50, Status = "Slobodna" },
                new Room { RoomId = 2, RoomNumber = "102", Capacity = 3, PricePerNight = 75, Status = "Zauzeta" },
                new Room { RoomId = 3, RoomNumber = "103", Capacity = 1, PricePerNight = 40, Status = "Slobodna" },
                new Room { RoomId = 4, RoomNumber = "104", Capacity = 2, PricePerNight = 60, Status = "Slobodna" }
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    ReservationId = 1,
                    UserId = 3,
                    RoomId = 2,
                    StartDate = DateTime.Now.AddDays(-1),
                    EndDate = DateTime.Now.AddDays(2),
                    Status = "Potvrđena"
                },
                new Reservation
                {
                    ReservationId = 2,
                    UserId = 3,
                    RoomId = 1,
                    StartDate = DateTime.Now.AddDays(3),
                    EndDate = DateTime.Now.AddDays(6),
                    Status = "Na čekanju"
                }
            );
        }
    }
}
