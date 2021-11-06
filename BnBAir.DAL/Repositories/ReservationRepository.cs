using System.Collections.Generic;
using System.Linq;
using BnBAir.DAL.EF;
using BnBAir.DAL.Enitities;
using Microsoft.EntityFrameworkCore;

namespace BnBAir.DAL.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>
    {
        private readonly ReservationContext _db;
        public ReservationRepository(ReservationContext db)
            : base(db, db.Reservations)
        {
            _db = db;
        }
        
        public override IEnumerable<Reservation> GetAll()
        {
            return _db.Reservations
                           .Include(reservation => reservation.Guest)
                           .Include(reservation => reservation.Room)
                           .ThenInclude(room=>room.Category);
        }
    }
}