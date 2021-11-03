using BnBAir.DAL.Enitities;
using BnBAir.DAL.InitializeData;
using Microsoft.EntityFrameworkCore;

namespace BnBAir.DAL.EF
{
    public sealed class ReservationContext : DbContext 
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryDate> CategoryDates { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        
        public ReservationContext(DbContextOptions<ReservationContext> options) 
            :base(options)
        {
        }
    }
}