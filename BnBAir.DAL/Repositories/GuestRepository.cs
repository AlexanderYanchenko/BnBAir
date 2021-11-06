using System.Collections.Generic;
using BnBAir.DAL.EF;
using BnBAir.DAL.Enitities;
using Microsoft.EntityFrameworkCore;

namespace BnBAir.DAL.Repositories
{
    public class GuestRepository : GenericRepository<Guest>
    {
        private readonly ReservationContext _db;
        public GuestRepository(ReservationContext db)
            : base(db, db.Guests)
        {
            _db = db;
        }
        public override IEnumerable<Guest> GetAll()
        {
            return _db.Guests.Include(guest => guest.Reservations);
        }
    }
}