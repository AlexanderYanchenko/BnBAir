using BnBAir.DAL.EF;
using BnBAir.DAL.Enitities;

namespace BnBAir.DAL.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>
    {
        public ReservationRepository(ReservationContext db)
            : base(db, db.Reservations)
        {
        }
    }
}