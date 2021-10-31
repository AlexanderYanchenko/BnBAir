using System;
using BnBAir.DAL.EF;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BnBAir.DAL.Repositories
{
    public class BnBAirUW : IBnBAirUW
    {
        private readonly ReservationContext _db;
        private CategoryDateRepository _categoryDateRepository;
        private IRepository<Category> _categoryRepository;
        private IRepository<Guest> _guestRepository;
        private IRepository<Reservation> _reservationRepository;
        private IRepository<Room> _roomRepository;
        
        private bool _disposed = false;
        public BnBAirUW(DbContextOptions<ReservationContext> options)
        {
            _db = new ReservationContext(options);
        }

        public IRepository<CategoryDate> CategoryDates => _categoryDateRepository ??= new CategoryDateRepository(_db);

        public IRepository<Category> Categories => _categoryRepository ??= new CategoryRepository(_db);

        public IRepository<Guest> Guests => _guestRepository ??= new GuestRepository(_db);

        public IRepository<Reservation> Reservations => _reservationRepository ??= new ReservationRepository(_db);

        public IRepository<Room> Rooms => _roomRepository ??= new RoomRepository(_db);
        
        public void Save()
        {
            this._db.SaveChanges();
        }
        
        public void Dispose()
        {
            if (!this._disposed)
            {
                _db.Dispose();
            }

            this._disposed = true;
        }
        
    }
}