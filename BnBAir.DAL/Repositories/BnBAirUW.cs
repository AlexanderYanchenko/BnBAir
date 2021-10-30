using System;
using BnBAir.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace BnBAir.DAL.Repositories
{
    public class BnBAirUW : IDisposable
    {
        private ReservationContext _db;
        private CategoryDateRepository _categoryDateRepository;
        private CategoryRepository _categoryRepository;
        private GuestRepository _guestRepository;
        private ReservationRepository _reservationRepository;
        private RoomRepository _roomRepository;
        
        public BnBAirUW(DbContextOptions<ReservationContext> options)
        {
            _db = new ReservationContext(options);
        }

        public CategoryDateRepository CategoryDates
        {
            get
            {
                if (_categoryDateRepository == null)
                {
                    _categoryDateRepository = new CategoryDateRepository(_db);
                }

                return _categoryDateRepository;
            }
        }
        
        public CategoryRepository Categories
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_db);
                }

                return _categoryRepository;
            }
        }
        
        public GuestRepository Guests
        {
            get
            {
                if (_guestRepository == null)
                {
                    _guestRepository = new GuestRepository(_db);
                }

                return _guestRepository;
            }
        }
        
        public ReservationRepository Reservations
        {
            get
            {
                if (_reservationRepository == null)
                {
                    _reservationRepository = new ReservationRepository(_db);
                }

                return _reservationRepository;
            }
        }
        
        public RoomRepository Rooms
        {
            get
            {
                if (_roomRepository == null)
                {
                    _roomRepository = new RoomRepository(_db);
                }

                return _roomRepository;
            }
        }

        public void Save()
        {
            this._db.SaveChanges();
        }
        
        public void Dispose()
        {
        }
        
    }
}