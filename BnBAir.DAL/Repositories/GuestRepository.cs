using System.Collections.Generic;
using System.Threading.Tasks;
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
        public override async Task<IEnumerable<Guest>> GetAll()
        {
            return await _db.Guests
                         .Include(guest => guest.Reservations)
                         .ThenInclude(reservation=>reservation.Room)
                         .ToListAsync();
        }
    }
}