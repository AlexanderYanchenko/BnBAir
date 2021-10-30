using System;
using BnBAir.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace BnBAir.DAL.Repositories
{
    public class BnBAirUW : IDisposable
    {
        private readonly ReservationContext _db;
        private CategoryDateRepository _categoryDateRepository;
        private CategoryRepository _categoryRepository;
        private GuestRepository _guestRepository;
        private ReservationRepository _reservationRepository;
        private RoomRepository _roomRepository;
        
        private bool _disposed = false;
        public BnBAirUW(DbContextOptions<ReservationContext> options)
        {
            _db = new ReservationContext(options);
        }

        public CategoryDateRepository CategoryDates => _categoryDateRepository ??= new CategoryDateRepository(_db);

        public CategoryRepository Categories => _categoryRepository ??= new CategoryRepository(_db);

        public GuestRepository Guests => _guestRepository ??= new GuestRepository(_db);

        public ReservationRepository Reservations => _reservationRepository ??= new ReservationRepository(_db);

        public RoomRepository Rooms => _roomRepository ??= new RoomRepository(_db);

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