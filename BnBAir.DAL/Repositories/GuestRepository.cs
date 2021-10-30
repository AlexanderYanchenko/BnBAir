using BnBAir.DAL.EF;
using BnBAir.DAL.Enitities;

namespace BnBAir.DAL.Repositories
{
    public class GuestRepository : GenericRepository<Guest>
    {
        public GuestRepository(ReservationContext db)
            : base(db, db.Guests)
        {
        }
    }
}